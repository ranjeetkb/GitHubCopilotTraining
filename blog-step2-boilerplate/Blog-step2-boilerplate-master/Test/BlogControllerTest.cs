using System.Collections.Generic;
using Blog_Step2.Controllers;
using Blog_Step2.Models;
using Blog_Step2.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Test
{

    [TestCaseOrderer("Test.PriorityOrderer", "test")]
    public class BlogControllerTest
    {
        [Fact,TestPriority(0)]
        public void GetShouldReturnListOfBlogPosts()
        {
            var mockRepo = new Mock<IBlogPostRepository>();
            mockRepo.Setup(repo => repo.GetBlogPosts()).Returns(this.blogs);
            var blogController = new BlogPostController(mockRepo.Object);

            var actual = blogController.Index();

            var actionResult = Assert.IsType<ViewResult>(actual);
            Assert.IsAssignableFrom<IEnumerable<BlogPost>>(actionResult.ViewData.Model);
        }

        [Fact, TestPriority(1)]
        public void CreateShouldReturnListOfBlogPost_ModelIsValid()
        {
            var blog = new BlogPost { PostId = 1, Title = "Sample Blog Post", Content = "This is a sample blog post content", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, PublishedAt = DateTime.Now, Slug = "sample-blog-post" };
            var mockRepo = new Mock<IBlogPostRepository>();
            mockRepo.Setup(repo => repo.CreateBlogPost(blog));
            var blogController = new BlogPostController(mockRepo.Object);

            var actual = blogController.Create(blog);

            var actionResult = Assert.IsType<RedirectToActionResult>(actual);
            Assert.Null(actionResult.ControllerName);
            Assert.Equal("Index", actionResult.ActionName);
        }

        [Fact, TestPriority(2)]
        public void CreateShouldReturn_ModelIsInValid()
        {
            var blog = new BlogPost { PostId = 1, Title = "Sample Blog Post", Content = "This is a sample blog post content", CreatedAt = DateTime.Now.AddDays(-2), UpdatedAt = DateTime.Now.AddDays(-1), PublishedAt = DateTime.Now, Slug = "sample-blog-post" };
            var mockRepo = new Mock<IBlogPostRepository>();
            mockRepo.Setup(repo => repo.CreateBlogPost(blog));
            var blogController = new BlogPostController(mockRepo.Object);
            blogController.ModelState.AddModelError("Title", "Required");

            var actual = blogController.Create(blog);

            var actionResult = Assert.IsType<ViewResult>(actual);
            Assert.IsAssignableFrom<BlogPost>(actionResult.ViewData.Model);
            Assert.Same(actionResult.ViewData.Model, blog);
        }

        [Fact, TestPriority(3)]
        public void DeleteShouldReturnToIndex()
        {
            int PostId = 1;
            var mockRepo = new Mock<IBlogPostRepository>();
            mockRepo.Setup(repo => repo.DeleteBlogPost(PostId)).Returns(1);
            var blogController = new BlogPostController(mockRepo.Object);

            var actual = blogController.Delete(PostId);

            var actionResult = Assert.IsType<RedirectToActionResult>(actual);
            Assert.Null(actionResult.ControllerName);
            Assert.Equal("Index", actionResult.ActionName);
        }

        [Fact, TestPriority(4)]
        public void UpdateShouldReturnToIndex()
        {
            var mockRepo = new Mock<IBlogPostRepository>();
            var post = new BlogPost { PostId = 1, Title = "Sample Blog Post", Content = "This is a sample blog post content", CreatedAt = DateTime.Now.AddDays(-2), UpdatedAt = DateTime.Now.AddDays(-1), PublishedAt = DateTime.Now, Slug = "sample-blog-post" };
            mockRepo.Setup(repo => repo.UpdateBlogPost(post)).Returns(1);
            var blogController = new BlogPostController(mockRepo.Object);

            var actual = blogController.Edit(post);

            var actionResult = Assert.IsType<RedirectToActionResult>(actual);
            Assert.Null(actionResult.ControllerName);
            Assert.Equal("Index", actionResult.ActionName);
        }

        [Fact, TestPriority(5)]
        public void DetailsShouldReturnBlogPostById()
        {
            int PostId = 1;
            var mockRepo = new Mock<IBlogPostRepository>();
            mockRepo.Setup(repo => repo.GetBlogPostById(PostId)).Returns((BlogPost)null);
            var blogController = new BlogPostController(mockRepo.Object);

            var actual = blogController.Details(PostId) as NotFoundResult;

            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
        }

        private readonly List<BlogPost> blogs = new List<BlogPost> {
            new BlogPost {PostId = 1, Title = "Sample Blog Post", Content="This is a sample blog post content",CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now,PublishedAt=DateTime.Now,Slug="sample-blog-post"},
            new BlogPost {PostId = 2, Title = "Another Sample Blog Post", Content="This is a another sample blog post content",CreatedAt=DateTime.Now.AddDays(-2),UpdatedAt=DateTime.Now,PublishedAt=DateTime.Now,Slug="another-sample-blog-post"}
    };
    }
}
