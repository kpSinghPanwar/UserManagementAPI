
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(int id);
        User AddUser(User user);
        bool UpdateUser(int id, User user);
        bool DeleteUser(int id);
    }
}
