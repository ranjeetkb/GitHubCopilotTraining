using Moq;
using Commander.TestSetup;
using UserService.Models;
using UserService.Repository;
using UserService.Service;


namespace ServiceTests
{
    [TestCaseOrderer("TestSetup.PriorityOrderer", "commander")]
    public class UserServiceTest
    {
       
        private Mock<IUserRepository> userRepositoryMock;        
        private IUserService userService;
       
        public UserServiceTest()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            userService =new UserService.Service.UserService(userRepositoryMock.Object);            

        }

        #region UserServiceTests    

        [Fact,TestPriority(1)]      
        public void CreateUser_ShouldCreateNewUser()
        {
            // Arrange           
            // Arrange         
            var user = new User() { UserId = 1, UserName = "User1", Email = "user1@gmail.com", Password = "Password123" };
            userRepositoryMock.Setup(repo => repo.CreateUser(user)).Returns(user);

            // Act
            var result = userService.CreateUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<User>(result);
            userRepositoryMock.Verify(repo => repo.CreateUser(user), Times.Once);
        }

        [Fact, TestPriority(2)]
        public void GetAllUsers_ReturnsAllUsers()
        {
            // Arrange

            var expectedUsers = new List<User>
            {
            new User() { UserId = 1, UserName = "User1", Email = "user1@gmail.com", Password = "Password123" },
            new User() { UserId = 2, UserName = "User2", Email = "user2@gmail.com", Password = "Password123" }
            };
            userRepositoryMock.Setup(repo => repo.GetAllUsers()).Returns(expectedUsers);

            // Act
            var result = userService.GetAllUsers();

            // Assert
            Assert.IsAssignableFrom<List<User>>(result);
            Assert.Equal(expectedUsers, result);
            Assert.Equal(2, expectedUsers.Count);

        }

        [Fact, TestPriority(3)]
        public void GetUserById_ReturnsUserWithValidId()
        {
            // Arrange
            int id = 1;
            var expectedUser = new User() { UserId = 1, UserName = "User1", Email = "user1@gmail.com", Password = "Password123" };

            userRepositoryMock.Setup(repo => repo.GetUserById(id)).Returns(expectedUser);


            // Act
            var result = userService.GetUserById(id);

            // Assert
            Assert.IsAssignableFrom<User>(result);
            Assert.Equal(expectedUser, result);
            Assert.Equal("User1", result.UserName);
        }

        [Fact, TestPriority(4)]
        public void UpdateUser_ReturnsTrueOnSuccess()
        {
            // Arrange
            int id = 1;
            var updateduser = new User() { UserId = 1, UserName = "User1", Email = "user1@gmail.com", Password = "Password123" };

            userRepositoryMock.Setup(repo => repo.UpdateUser(id,updateduser)).Returns(true);

            // Act
            var result = userService.UpdateUser(id, updateduser);

            // Assert
            Assert.True(result);
            userRepositoryMock.Verify(repo => repo.UpdateUser(id, updateduser), Times.Once);
        }

        [Fact, TestPriority(5)]
        public void DeleteUser_ReturnsTrueOnSuccess()
        {
            // Arrange
            int id = 1;
            userRepositoryMock.Setup(repo => repo.DeleteUser(id)).Returns(true);

            // Act
            var result = userService.DeleteUser(id);

            // Assert
            Assert.True(result);
            userRepositoryMock.Verify(repo => repo.DeleteUser(id), Times.Once);
        }
        #endregion UserServiceTests

    }
}
