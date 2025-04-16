using MongoDB.Driver;
using UserService.Exceptions;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly UserDbSettings _settings;
        public UserRepository(UserDbSettings dbSettings)
        {
           _settings = dbSettings;
           _usersCollection = _settings.usersCollection;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _usersCollection.Find(_ => true).ToList();
        }


        public User GetUserById(int id)
        {
            var user = _usersCollection.Find(user => user.UserId == id).FirstOrDefault();
            if (user == null)
                throw new UserNotFoundException($"User with id {id} not found");
            return user;
        }

        public User CreateUser(User user)
        {
            try
            {
                _usersCollection.InsertOne(user);
                return user;
            }
            catch (Exception)
            {
                throw new UserAlreadyExistsException($"User with id {user.UserId} already exixts");
            }
        }

        public bool UpdateUser(int id, User user)
        {
            var result = _usersCollection.ReplaceOne(u => u.UserId == id, user);
            if (result.ModifiedCount == 0)
                throw new UserNotFoundException($"User with id {id} not found");
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public bool DeleteUser(int id)
        {
            var result = _usersCollection.DeleteOne(u => u.UserId == id);
            if (result.DeletedCount == 0)
                throw new UserNotFoundException($"User with id {id} not found");
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

       
    }
}
