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

namespace Commander.RepositoryTests
{
    [TestCaseOrderer("Commander.PriorityOrderer", "commander")]
    public class UserRepositoryTest
    {
        private DbContextOptions<BlogDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase("BlogDatabase")
                .Options;
        }

        [Fact, TestPriority(1)]
        public void Create_ValidUser_ReturnsCreatedUser()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new UserRepository(mockcontext);
                var user1 = new User
                {
                    UserId = 1,
                    Username = "user1",
                    Email = "user1@test.com",
                    

                };

                // Act
                var createdUser = repository.Create(user1);
                mockcontext.SaveChanges();

                // Assert
                Assert.NotNull(createdUser);
                Assert.Equal(user1.UserId, createdUser.UserId);
                Assert.Equal(user1.Username, createdUser.Username);
                Assert.Equal(user1.Email, createdUser.Email);
            }
        }

        [Fact, TestPriority(2)]
        public void GetById_ExistingId_ReturnsCorrectUser()
        {
            // Arrange
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new UserRepository(mockcontext);
                var userid = 1;

                // Act
                var retrievedUser = repository.GetById(userid);

                // Assert
                Assert.NotNull(retrievedUser);
                Assert.Equal("user1", retrievedUser.Username);
                Assert.Equal("user1@test.com", retrievedUser.Email);
            }
        }

        [Fact, TestPriority(3)]
        public void GetAll_ReturnsAllUsers()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new UserRepository(mockcontext);

                var user2 = new User
                {
                    UserId = 2,
                    Username = "user2",
                    Email = "user2@test.com",
                };
                var user3 = new User
                {
                    UserId = 3,
                    Username = "user3",
                    Email = "user3@test.com",
                };

                repository.Create(user2);
                repository.Create(user3);


                mockcontext.SaveChanges();

                // Act
                var count = mockcontext.Users.Count();
                var allUsers = repository.GetAll();

                // Assert
                Assert.Equal(count, allUsers.Count());
            }
        }

        [Fact, TestPriority(4)]
        public void Update_ExistingUser_ModifiesUser()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new UserRepository(mockcontext);
                var userid = 1;

                var updatedUser = new User { UserId = 1, Username = "user1", Email = "user1@testdomain.com" };


                // Act
                repository.Update(updatedUser);
                mockcontext.SaveChanges();

                var existingUser = mockcontext.Users.Find(userid);

                // Assert                
                Assert.Equal(updatedUser.UserId, existingUser.UserId);
                Assert.Equal(updatedUser.Username, existingUser.Username);
                Assert.Equal(updatedUser.Email, existingUser.Email);
            }
        }

        [Fact, TestPriority(5)]
        public void Delete_ExistingUser_DeletesUser()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new UserRepository(mockcontext);

                var userid = 1;

                // Act
                repository.Delete(userid);
                mockcontext.SaveChanges();

                // Assert
                var deletedUser = mockcontext.Users.FirstOrDefault(u => u.UserId == userid);
                Assert.Null(deletedUser);


            }
        }
    }
}
