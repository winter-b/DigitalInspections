using DigitalInspectionsWebApi.Models;

namespace DigitalInspectionsWebApi.Interfaces
{
    public interface IAuthRepository
    {
        User GetUserInfoByName(string username);
        bool Register(User user);
        bool UpdateCredentials(User user, string newPassword);
    }
}
