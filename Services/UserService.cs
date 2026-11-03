
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new()
        {
            new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            }
        };

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public User? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }


        public User AddUser(User user)
        {
            if (_users.Any(u =>
                u.Email.Equals(user.Email,
                StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(
                    "A user with this email already exists.");
            }

            user.Id = _users.Any()
                ? _users.Max(u => u.Id) + 1
                : 1;

            _users.Add(user);

            return user;
        }

        public bool UpdateUser(int id, User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return false;
            }

            if (_users.Any(u =>
                u.Id != id &&
                u.Email.Equals(user.Email,
                StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(
                    "A user with this email already exists.");
            }

            existingUser.FirstName = user.FirstName.Trim();
            existingUser.LastName = user.LastName.Trim();
            existingUser.Email = user.Email.Trim();

            return true;
        }
        public bool DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return false;
            }
            _users.Remove(user);
            return true;
        }
    }
}
