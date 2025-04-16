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
    public class CommentControllerTest    
    {
        [Fact,TestPriority(1)]
        public void CreateComment_ShouldReturnOkResult()
        {
            // Arrange
            var commentRepositoryMock = new Mock<CommentRepository>();            

            var controller = new CommentController(commentRepositoryMock.Object);

            // Act
            var result = controller.CreateComment(new Comment ( 1,"Test Comment 1",DateTime.Now,1 ));

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var comment = Assert.IsType<Comment>(okResult.Value);
            Assert.Equal("Test Comment 1", comment.Content);
        }

        [Fact,TestPriority(1)]
        public void GetCommentById_CommentExists_ShouldReturnComment()
        {
            // Arrange
            var commentRepositoryMock = new Mock<CommentRepository>();               
            var controller = new CommentController(commentRepositoryMock.Object);
            var newcomment = controller.CreateComment(new Comment(1, "Test Comment 1", DateTime.Now, 1));
            var Id = 1;
            // Act
            var result = controller.GetCommentById(Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);            
           
        }

        [Fact, TestPriority(3)]
        public void GetCommentsForPost_ShouldReturnCommentsForPost()
        {
            // Arrange
            var commentRepositoryMock = new Mock<CommentRepository>();
            var controller = new CommentController(commentRepositoryMock.Object);
            var postId = 1;

            // Act
            controller.CreateComment(new Comment(1, "Test Comment 1", DateTime.Now.AddDays(-2), 1));
            controller.CreateComment(new Comment(2, "Test Comment 2", DateTime.Now.AddDays(-1), 1));
            var result = controller.GetCommentsForPost(postId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedComments = Assert.IsAssignableFrom<List<Comment>>(okResult.Value);
           
        }

        [Fact,TestPriority(4)]
        public void UpdateComment_ShouldReturnOkResult()
        {
            // Arrange
            var commentRepositoryMock = new Mock<CommentRepository>();           
            var controller = new CommentController(commentRepositoryMock.Object);
            var updatedComment = new Comment(1, "Updated Test Comment 1", DateTime.Now.AddDays(-2), 1);
            // Act
            controller.CreateComment(new Comment(1, "Test Comment 1", DateTime.Now.AddDays(-2), 1));
            var result = controller.UpdateComment(1, updatedComment); 

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact, TestPriority(4)]
        public void DeleteComment_ShouldReturnOkResult()
        {
            // Arrange
            var commentRepositoryMock = new Mock<CommentRepository>();
            var controller = new CommentController(commentRepositoryMock.Object);
            var Id = 1;
            // Act
            
            var result = controller.DeleteComment(Id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

    }
}
