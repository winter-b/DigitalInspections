using static DigitalInspectionsWebApi.Models.User;

namespace DigitalInspectionsWebApi.Interfaces
{
    public interface IAuthService
    {
        string GetUsernameFromToken(string token);
        string Login(string username, string password);
        bool Register(string username, string password, Role role);
        bool UpdateCredentials(string username, string oldPassword, string newPassword);
        Guid GetUserIdFromName(string username);
        bool UserTokenValid(string token, string v);
        string EncryptPassword(string password);
        bool RegistrationCodeValid(string key);
        bool IsPasswordExpired(string username);
    }
}
