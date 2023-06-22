using DigitalInspectionsWebApi.Helpers;
using DigitalInspectionsWebApi.Interfaces;
using DigitalInspectionsWebApi.Models;

namespace DigitalInspectionsWebApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DigitalInspectionsDbContext _dbContext;

        public AuthRepository(DigitalInspectionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserInfoByName(string username)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username == username);
        }

        public bool Register(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateCredentials(User user, string newPassword)
        {
            user.Password = newPassword;
            _dbContext.SaveChanges();
            return true; 
        }
        
    }
}
