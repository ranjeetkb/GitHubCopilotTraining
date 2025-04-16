using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using CustomExceptions;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Blog.Controllers;

namespace Commander.ControllerTests
{
    [TestCaseOrderer("Commander.PriorityOrderer", "commander")]
    public class UserControllerTest
    {
        [Fact, TestPriority(1)]
        public void Get_Returns_ObjectResult_WhenUserExist()
        {
            // Arrange
            var mockUserService = new Mock<IRepository<User>>();
            mockUserService.Setup(repo => repo.GetAll()).Returns(this.users);
            var controller = new UserController(mockUserService.Object);

            // Act
            var result = controller.GetUsers();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact,TestPriority(2)]
        public void Post_Returns_ObjectResult_WhenValidUser()
        {
            // Arrange
            var mockUserService = new Mock<IRepository<User>>();
            var newUser = new User { UserId = 3, Username = "user3", Email = "user3@test.com" };
            mockUserService.Setup(repo => repo.Create(newUser));
            var controller = new UserController(mockUserService.Object);
            

            // Act
            var result = controller.PostUser(newUser);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
       


        [Fact,TestPriority(3)]
        public void GetById_ReturnsOkResult_WhenUserExists()
        {
            // Arrange
            var mockUserService = new Mock<IRepository<User>>();
            var UserId = 1;
            mockUserService.Setup(repo => repo.GetById(UserId)).Returns((User)null);
            var controller = new UserController(mockUserService.Object);

            // Act
            var result = controller.GetUser(UserId);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(result);
        }

        [Fact,TestPriority(4)]
        public void Put_ReturnsNoContentResult_WhenValidUser()
        {
            // Arrange
            var mockUserService = new Mock<IRepository<User>>();
            var existingUser = new User { UserId = 1, Username = "newuser1", Email = "newuser1@test.com" };
            mockUserService.Setup(repo => repo.Update(existingUser));
            var controller = new UserController(mockUserService.Object);
            

            // Act
            var result = controller.PutUser(1, existingUser);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact,TestPriority(5)]
        public void Delete_ReturnsNoContentResult_WhenUserExists()
        {
            // Arrange
            var mockUserService = new Mock<IRepository<User>>();
            var UserId = 1;
            mockUserService.Setup(repo => repo.Delete(UserId));
            var controller = new UserController(mockUserService.Object);

            // Act
            var result = controller.DeleteUser(UserId);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(result);
        }

        private readonly List<User> users=new List<User>()
        {
             new User { UserId = 1, Username = "user1", Email = "user1@test.com" },
             new User { UserId = 2, Username = "user2", Email = "user2@test.com" }
    };

    }
}
