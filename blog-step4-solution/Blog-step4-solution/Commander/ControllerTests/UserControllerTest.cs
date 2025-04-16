using Commander.TestSetup;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using UserService.Controllers;
using UserService.Models;
using UserService.Repository;
using UserService.Service;

namespace ControllerTests
{

    [TestCaseOrderer("TestSetUp.PriorityOrderer", "commander")]
    public class UserControllerTest : IClassFixture<UserControllerFixture>
    {
        private IUserRepository userRepository;
        private IUserService userService;
        private IMongoCollection<User> usercollection;
        UserController controller;
        public UserControllerTest(UserControllerFixture fixture)
        {
            userRepository = new UserRepository(fixture.usercontext);
            userService = new UserService.Service.UserService(userRepository);
            usercollection = fixture.usercontext.usersCollection;
            controller = new UserController(userService);
        }

        #region UserController Tests
        [Fact, TestPriority(1)]
        public void CreateUser_ReturnsCreatedAtActionResult_WithNewUser()
        {
            // Arrange        

            var newuser = new User { UserId = 101, UserName = "User101", Email = "user101@gmail.com", Password = "Password123" };

            // Act
            var result = controller.CreateUser(newuser) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedAtActionResult>(result);
            var createduser = usercollection.Find(x => x.UserId == 101).FirstOrDefault();
            Assert.NotNull(createduser);

        }

        [Fact, TestPriority(2)]
        public void GetAllUsers_ReturnsOkResult_WithListOfUsers()
        {

            // Act
            var result = controller.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<IEnumerable<User>>>(result);
            Assert.IsType<ActionResult<IEnumerable<User>>>(result);
        }

        [Fact, TestPriority(3)]
        public void GetUserForValidID_ReturnsOkResult_WithUser()
        {
            //Arrange
            var userid = 1;

            // Act
            var result = controller.GetUserById(userid);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<User>>(result);
            Assert.IsType<ActionResult<User>>(result);
            var founduser = usercollection.Find(x => x.UserId == userid).FirstOrDefault();
            Assert.NotNull(founduser);
        }

        [Fact, TestPriority(4)]
        public void UpdateUser_WithValidId_ReturnsNoContent()
        {
            //Arrange
            var userid = 1;
            var userToUpdate = new User { UserId = 1, UserName = "User1", Email = "user1@test.com", Password = "Password@123" };

            // Act
            var result = controller.UpdateUser(userid, userToUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);

        }

        [Fact, TestPriority(5)]
        public void DeleteUser_WithValidId_ReturnsNoContent()
        {
            //Arrange
            var userid = 2;


            // Act
            var result = controller.DeleteUser(userid);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);

        }
        #endregion UserController Tests
    }

}
