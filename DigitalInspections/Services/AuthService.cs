using DigitalInspectionsWebApi.Interfaces;
using DigitalInspectionsWebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static DigitalInspectionsWebApi.Models.User;

namespace DigitalInspectionsWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authorizationRepository;
        private string _passwordSalt;
        private string _passwordPrivateKey;
        private string _registrationKey;

        public AuthService(IAuthRepository authRepository, string passwordSalt, string passwordPrivateKey, string registrationKey)
        {
            _passwordSalt = passwordSalt;
            _passwordPrivateKey = passwordPrivateKey;
            _registrationKey = registrationKey;
            _authorizationRepository = authRepository;
        }

        public string Login(string username, string password) 
        {
            string token = "";
            try
            {
                var securePassword = EncryptPassword(password);
                User userInfo = _authorizationRepository.GetUserInfoByName(username);
                if (userInfo.Password == securePassword)
                {
                    token = GenerateToken(username, _passwordPrivateKey, userInfo.UserRole.ToString());
                }
            }
            catch (Exception e){ 
                Console.WriteLine(e);
            }

            return token;
        }

        public string GenerateToken(string username, string privateKey, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(privateKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name , username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) 
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);  
            return tokenHandler.WriteToken(token);
        }

        public byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            //create SHA256 object
            HashAlgorithm algorithm = SHA256.Create();
            byte[] plainTextWithSaltBytes = 
                new byte[plainText.Length + salt.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }
            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                string encryptedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return encryptedPassword;
            }
        }

        public string GetUsernameFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_passwordPrivateKey);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key), 
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return principal.Identity?.Name ?? "";
        }

        public Guid GetUserIdFromName(string username)
        {
            User user = _authorizationRepository.GetUserInfoByName(username);
            return user.Id;
        }

        public bool UpdateCredentials(string username, string oldPassword, string newPassword)
        {
            User user = _authorizationRepository.GetUserInfoByName(username);
            oldPassword = EncryptPassword(oldPassword);
            newPassword = EncryptPassword(newPassword);
            if (user.Password == oldPassword && oldPassword != newPassword)
            {
                _authorizationRepository.UpdateCredentials(user, newPassword);
                return true;
            }
            return false;
        }

        public bool Register(string username, string password, Role role)
        {
            
            User user = _authorizationRepository.GetUserInfoByName(username);
            if (user == null)
            {
                Console.WriteLine("Encrypting");
                var securePassword = EncryptPassword(password);
                User newUser = new User {
                    Id = Guid.NewGuid(),
                    Username = username,
                    Password = securePassword,
                    UserRole = role,
                    LastUpdated = DateTime.MinValue

                };
                _authorizationRepository.Register(newUser);
                return true;
            }
            return false;
        }

        public bool UserTokenValid(string token, string role)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_passwordPrivateKey);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                if (principal.Identity?.Name != null)
                {
                    string userRole = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault() ?? "";
                    if (userRole == role || userRole == "Manager")
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RegistrationCodeValid(string key)
        {
            if (key == _registrationKey)
            {
                return true;
            }
            return false;
        }

        public bool IsPasswordExpired(string username)
        {
            User user = _authorizationRepository.GetUserInfoByName(username);
            if (user.LastUpdated == DateTime.MinValue)
            {
                return true;
            }
            else if (user.LastUpdated.AddDays(180) < DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}
