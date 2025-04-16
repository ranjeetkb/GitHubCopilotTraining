using Commander.TestSetup;
using MongoDB.Driver;
using UserService.Exceptions;
using UserService.Models;
using UserService.Repository;

namespace RepositoryTests
{
    [TestCaseOrderer("TestSetup.PriorityOrderer", "commander")]
    public class UserRepositoryTest:IClassFixture<UserDbFixture>
    {
        private IUserRepository userRepository;
        private IMongoCollection<User> usercollection;

        public UserRepositoryTest(UserDbFixture userDbFixture)
        {
            userRepository = new UserRepository(userDbFixture.context);
            usercollection = userDbFixture.context.usersCollection;
        }
        #region Positive UserRepositoryTests      

        [Fact,TestPriority(1)]
        public void CreateUser_ShouldCreateNewUser()
        {
            // Arrange         
            var user3 = new User() { UserId=3,UserName = "user3", Email = "user3@gmail.com", Password = "Password123" };
     
            // Act
            var result=userRepository.CreateUser(user3);

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<User>(result);
            Assert.Equal(3, result.UserId);
            Assert.Equal("user3", result.UserName);
            Assert.Equal("user3@gmail.com", result.Email);           

        }

        [Fact,TestPriority(2)]
        public void GetAllUsers_ReturnsAllUsers()
        {
            // Act
            var users = userRepository.GetAllUsers();

            // Assert
            Assert.NotNull(users);                  
            Assert.Equal(usercollection.CountDocuments(FilterDefinition<User>.Empty), users.Count());          

        }

        [Fact, TestPriority(3)]
        public void GetUserById_ReturnsUserWithValidId()
        {
            // Arrange
            var userId = 1;

            // Act
            var user = userRepository.GetUserById(userId);

            // Assert
            Assert.NotNull(user);
            Assert.IsAssignableFrom<User>(user);
            Assert.Contains("User1",user.UserName);
        }

        [Fact, TestPriority(4)]
        public void UpdateUser_ReturnsTrueOnSuccess()
        {
            //Arrange
            int userId = 1;
            var userToUpdate = new User { UserId = userId, UserName = "User1", Email = "updateduser1@test.com", Password = "Password123" };

            // Act
            var result = userRepository.UpdateUser(id: userId, userToUpdate);

            // Assert
            Assert.True(result);            
            var updateduser=usercollection.Find(x => x.UserId == userId).FirstOrDefault();
            Assert.Contains("updateduser1@test.com", updateduser.Email);

        }

        [Fact, TestPriority(5)]
        public void DeleteUser_ReturnsTrueOnSuccess()
        {
            //Arrange
            int userId = 2;

            // Act
            var result = userRepository.DeleteUser(userId);

            // Assert
            Assert.True(result);            
            var deleteduser = usercollection.Find(x => x.UserId == userId).FirstOrDefault();
            Assert.Null(deleteduser);

        }
        #endregion Postive RepositoryTests

        #region Negative UserRepositoryTests      

        [Fact, TestPriority(6)]
        public void CreateUserWithExistingId_ShouldThrowException()
        {
            // Arrange         
            var newuser = new User() { UserId = 1, UserName = "user1", Email = "user1@gmail.com", Password = "Password123" };

            // Act && Assert            
            Assert.Throws<UserAlreadyExistsException>(() =>
            {
                userRepository.CreateUser(newuser);
            });
            
        }

        [Fact, TestPriority(7)]
        public void GetUserByInvalidId_ReturnsException()
        {
            // Arrange
            var userId = 100;

            // Act && Assert
            Assert.Throws<UserNotFoundException>(() =>
            {
                userRepository.GetUserById(userId);
            });
        }
        #endregion Negative UserRepositoryTests      
    }
}
