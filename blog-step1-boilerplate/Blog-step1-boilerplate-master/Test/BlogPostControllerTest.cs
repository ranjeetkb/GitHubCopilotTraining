using System.Collections.Generic;
using Blog_Step1.Controllers;
using Blog_Step1.Models;
using Blog_Step1.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Test
{
    public class BlogPostControllerTest
    {
        [Fact]
        public void GetShouldReturnListOfBlogPosts()
        {
            var mockRepo = new Mock<IBlogPostRepository>();
            mockRepo.Setup(repo => repo.GetBlogPosts()).Returns(this.blogs);
            var blogController = new BlogPostController(mockRepo.Object);

            var actual = blogController.Index();

            var actionResult = Assert.IsType<ViewResult>(actual);
            Assert.IsAssignableFrom<IEnumerable<BlogPost>>(actionResult.ViewData.Model);
        }

        [Fact]
        public void CreateShouldReturnListOfBlogPost_ModelIsValid()
        {
            var blog = new BlogPost { PostId = 1, Title = "Sample Blog Post", Content="This is a sample blog post content",CreatedAt=DateTime.Now,PublishedAt=DateTime.Now,Slug="sample-blog-post"};
            var mockRepo = new Mock<IBlogPostRepository>();
            mockRepo.Setup(repo => repo.CreateBlogPost(blog));
            var blogController = new BlogPostController(mockRepo.Object);

            var actual = blogController.Create(blog);

            var actionResult = Assert.IsType<RedirectToActionResult>(actual);
            Assert.Null(actionResult.ControllerName);
            Assert.Equal("Index", actionResult.ActionName);
        }

        [Fact]
        public void CreateShouldReturn_ModelIsInValid()
        {
            var blog = new BlogPost { PostId = 1, Title = "Sample Blog Post", Content = "This is a sample blog post content", CreatedAt = DateTime.Now, PublishedAt = DateTime.Now, Slug = "sample-blog-post" };
            var mockRepo = new Mock<IBlogPostRepository>();
            mockRepo.Setup(repo => repo.CreateBlogPost(blog));
            var blogController = new BlogPostController(mockRepo.Object);
            blogController.ModelState.AddModelError("Title", "Required");

            var actual = blogController.Create(blog);

            var actionResult = Assert.IsType<ViewResult>(actual);
            Assert.IsAssignableFrom<BlogPost>(actionResult.ViewData.Model);
            Assert.Same(actionResult.ViewData.Model, blog);
        }

        [Fact]
        public void DeleteShouldReturnToIndex()
        {
            int PostId = 1;
            var mockRepo = new Mock<IBlogPostRepository>();
            mockRepo.Setup(repo => repo.DeleteBlogPost(PostId)).Returns(true);
            var blogController = new BlogPostController(mockRepo.Object);

            var actual = blogController.Delete(PostId);

            var actionResult = Assert.IsType<RedirectToActionResult>(actual);
            Assert.Null(actionResult.ControllerName);
            Assert.Equal("Index", actionResult.ActionName);
        }

        [Fact]
        public void DetailsShouldReturnBlogPostById()
        {
            int PostId = 1;
            var mockRepo = new Mock<IBlogPostRepository>();
            mockRepo.Setup(repo => repo.GetBlogPostById(PostId)).Returns((BlogPost)null);
            var blogController = new BlogPostController(mockRepo.Object);

            var actual = blogController.Details(PostId) as NotFoundResult;

            Assert.NotNull(actual);
            Assert.Equal(404,actual.StatusCode);
        }

        private readonly List<BlogPost> blogs = new List<BlogPost> {
            new BlogPost {PostId = 1, Title = "Sample Blog Post", Content="This is a sample blog post content",CreatedAt=DateTime.Now,PublishedAt=DateTime.Now,Slug="sample-blog-post"},
            new BlogPost {PostId = 2, Title = "Another Sample Blog Post", Content="This is a another sample blog post content",CreatedAt=DateTime.Now.AddDays(-2),PublishedAt=DateTime.Now,Slug="another-sample-blog-post"}
    };
    }
}
