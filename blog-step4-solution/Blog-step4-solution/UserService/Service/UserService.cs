using UserService.Models;
using UserService.Repository;

namespace UserService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return  _userRepository.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return  _userRepository.GetUserById(id);
        }

        public User CreateUser(User user)
        {
            _userRepository.CreateUser(user);
            return user;
        }

        public bool UpdateUser(int id, User user)
        {
            return _userRepository.UpdateUser(id, user);
        }

        public bool DeleteUser(int id)
        {
            return  _userRepository.DeleteUser(id);
        }
    }
}
