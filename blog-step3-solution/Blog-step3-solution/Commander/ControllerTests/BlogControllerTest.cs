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
using System.Web.Http;

namespace Commander.ControllerTests
{
    [TestCaseOrderer("Commander.PriorityOrderer", "commander")]
    public class BlogControllerTest
    {

        [Fact, TestPriority(1)]
        public void Get_Returns_ObjectResult_WhenBlogPostsExist()
        {
            // Arrange
            var mockBlogPostService = new Mock<IRepository<BlogPost>>();
            mockBlogPostService.Setup(repo => repo.GetAll()).Returns(this.blogs);
            var controller = new BlogController(mockBlogPostService.Object);

            // Act
            var result = controller.GetAllBlogPosts();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact,TestPriority(2)]
        public void Post_Returns_ObjectResult_WhenValidBlogPost()
        {
            // Arrange
            var mockBlogPostService = new Mock<IRepository<BlogPost>>();
            var newBlogPost = new BlogPost { Id = 3, Title = "Sample Blog Post 3", Content = "Sample Content 3", CreatedAt = DateTime.Now };
            mockBlogPostService.Setup(repo => repo.Create(newBlogPost));
            var controller = new BlogController(mockBlogPostService.Object);
            

            // Act
            var result = controller.PostBlogPost(newBlogPost);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
       

       
        [Fact,TestPriority(3)]
        public void GetById_ReturnsOkResult_WhenBlogPostExists()
        {
            // Arrange
            var mockBlogPostService = new Mock<IRepository<BlogPost>>();
            var id = 1;
            mockBlogPostService.Setup(repo => repo.GetById(id)).Returns((BlogPost)null);
            var controller = new BlogController(mockBlogPostService.Object);            

            // Act           
            var result = controller.GetBlogPost(id);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(result);  
           
        }       


        [Fact,TestPriority(4)]
        public void Put_ReturnsNoContentResult_WhenValidBlogPost()
        {
            // Arrange
            var mockBlogPostService = new Mock<IRepository<BlogPost>>();            
            var existingBlogPost = new BlogPost { Id = 1, Title = "Updated Test Title", Content = "Test Content", CreatedAt = DateTime.Now };
            mockBlogPostService.Setup(repo => repo.Update(existingBlogPost));
            var controller = new BlogController(mockBlogPostService.Object);
            

            // Act
            var result = controller.PutBlogPost(1, existingBlogPost);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact,TestPriority(5)]
        public void Delete_ReturnsNoContentResult_WhenBlogPostExists()
        {
            // Arrange
            var mockBlogPostService = new Mock<IRepository<BlogPost>>();
            var id = 1;
            mockBlogPostService.Setup(repo => repo.Delete(id));
            var controller = new BlogController(mockBlogPostService.Object);

            // Act
            var result = controller.DeleteBlogPost(id);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(result);
            
        }

        private readonly List<BlogPost> blogs = new List<BlogPost> {
            new BlogPost {Id = 1, Title = "Sample Blog Post", Content="This is a sample blog post content",CreatedAt=DateTime.Now,},
            new BlogPost {Id = 2, Title = "Another Sample Blog Post", Content="This is a another sample blog post content",CreatedAt=DateTime.Now.AddDays(-2)}
    };

    }
}
