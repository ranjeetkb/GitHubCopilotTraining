using UserService.Models;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User CreateUser(User user);
        bool UpdateUser(int id, User user);
        bool DeleteUser(int id);
        

    }
}
