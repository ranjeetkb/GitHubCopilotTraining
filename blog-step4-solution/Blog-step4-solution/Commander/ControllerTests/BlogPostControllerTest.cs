using BlogPostService.Controllers;
using BlogPostService.Models;
using BlogPostService.Repository;
using BlogPostService.Service;
using Commander.TestSetup;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ControllerTests
{

    [TestCaseOrderer("TestSetUp.PriorityOrderer", "commander")]
    public class BlogPostControllerTest:IClassFixture<BlogControllerFixture>
    {
       
        private IBlogPostRepository blogPostRepository;
        private IBlogPostService blogPostService;        
        private IMongoCollection<BlogPost> blogpostcollection;
        BlogPostController controller;
        public BlogPostControllerTest(BlogControllerFixture fixture)
        {           
            blogPostRepository = new BlogPostRepository(fixture.blogcontext);
            blogPostService = new BlogPostService.Service.BlogPostService(blogPostRepository);              
            blogpostcollection = fixture.blogcontext.blogPostsCollection;           
            controller = new BlogPostController(blogPostService);           
        }

        #region BlogPostController Tests
        [Fact,TestPriority(1)]
        public void CreateBlogPost_ReturnsCreatedAtActionResult_WithNewBlogPost()
        {
            // Arrange        

            var newBlogPost = new BlogPost { BlogPostId=101,Title = "Test Post 101", Content = "Test Content for post 101",CreatedAt=DateTime.Now };

            // Act
            var result = controller.CreateBlogPost(newBlogPost) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedAtActionResult>(result);
            var createdblogpost=blogpostcollection.Find(x=>x.BlogPostId==101).FirstOrDefault();
            Assert.NotNull(createdblogpost);
            
        }

        [Fact,TestPriority(2)]
        public void GetAllBlogPosts_ReturnsOkResult_WithListOfBlogPosts()
        {

            // Act
            var result = controller.GetAllBlogPosts();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<IEnumerable<BlogPost>>>(result);
            Assert.IsType<ActionResult<IEnumerable<BlogPost>>>(result);
        }

        [Fact, TestPriority(3)]
        public void GetBlogPostForValidID_ReturnsOkResult_WithBlogPost()
        {
            //Arrange
            var blogpostid = 1;

            // Act
            var result = controller.GetBlogPostById(blogpostid);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<BlogPost>>(result);
            Assert.IsType<ActionResult<BlogPost>>(result);
            var foundblogpost=blogpostcollection.Find(x=>x.BlogPostId==blogpostid).FirstOrDefault();
            Assert.NotNull(foundblogpost);
        }

        [Fact, TestPriority(4)]
        public void UpdateBlogPost_WithValidId_ReturnsNoContent()
        {
            //Arrange
            var blogpostid = 1;
            var blogpostToUpdate= new BlogPost { BlogPostId = 1, Title = "Updated Test Post 1", Content = "Updated Test Content for post 101", CreatedAt = DateTime.Now };

            // Act
            var result = controller.UpdateBlogPost(blogpostid, blogpostToUpdate);

            // Assert
            Assert.NotNull(result);           
            Assert.IsType<NoContentResult>(result);
            
        }

        [Fact, TestPriority(5)]
        public void DeleteBlogPost_WithValidId_ReturnsNoContent()
        {
            //Arrange
            var blogpostid = 3;
            

            // Act
            var result = controller.DeleteBlogPost(blogpostid);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);

        }
        #endregion BlogPostController Tests
    }
}
