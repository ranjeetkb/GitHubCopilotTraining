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
    public class LikeControllerTest
    {
        [Fact, TestPriority(1)]
        public void CreateLike_ShouldReturnOkResult()
        {
            // Arrange
            var likeRepositoryMock = new Mock<LikeRepository>();

            var controller = new LikeController(likeRepositoryMock.Object);
            var like = new Like(1, 1);

            // Act
            var result = controller.CreateLike(like);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact, TestPriority(2)]
        public void GetLikesForPost_ShouldReturnLikesForPost()
        {
            // Arrange
            var postId = 2;
            var like1=new Like(1, postId);
            var like2=new Like(2, postId);

            var likeRepositoryMock = new Mock<LikeRepository>();

            var controller = new LikeController(likeRepositoryMock.Object);
            controller.CreateLike(like1);
            controller.CreateLike(like2);

            // Act
            var result = controller.GetLikesForPost(postId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedLikes = Assert.IsAssignableFrom<List<Like>>(okResult.Value);
            Assert.Equal(2, returnedLikes.Count);

        }

        [Fact,TestPriority(3)]
        public void GetLikesByUser_ShouldReturnLikesByUser()
        {
            // Arrange
            var userId = 1;
            var like1=new Like(userId,10);
            var like2 = new Like(userId, 20);

            var likeRepositoryMock = new Mock<LikeRepository>();            

            var controller = new LikeController(likeRepositoryMock.Object);

            // Act
            controller.CreateLike(like1);
            controller.CreateLike(like2);
            var result = controller.GetLikesByUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedLikes = Assert.IsAssignableFrom<List<Like>>(okResult.Value);
            Assert.Equal(2, returnedLikes.Count);
        }

        [Fact,TestPriority(4)]
        public void DeleteLike_ShouldReturnOkResult()
        {
            // Arrange
            var likeRepositoryMock = new Mock<LikeRepository>();            

            var controller = new LikeController(likeRepositoryMock.Object);
            var userId = 1;
            var postId = 10;

            // Act
            var result = controller.DeleteLike(userId, postId);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }


}


