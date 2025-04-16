using BlogPostService.Models;
using BlogPostService.Repository;
using BlogPostService.Service;
using Commander.TestSetup;
using Moq;
using UserService.Models;

namespace ServiceTests
{
    [TestCaseOrderer("TestSetup.PriorityOrderer", "commander")]
    public class BlogPostServiceTest
    {
       
        private Mock<IBlogPostRepository> blogPostRepositoryMock;
        private Mock<ISearchRepository> searchRepositoryMock;
        private IBlogPostService blogPostService;
        private ISearchService searchService;
        

        public BlogPostServiceTest()
        {
            blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            blogPostService =new BlogPostService.Service.BlogPostService(blogPostRepositoryMock.Object);

            searchRepositoryMock = new Mock<ISearchRepository>();
            searchService = new BlogPostService.Service.SearchService(searchRepositoryMock.Object);

        }

        #region BlogServiceTests    

        [Fact,TestPriority(1)]      
        public void CreateBlogPost_CallsRepositoryMethod()
        {
            // Arrange           
            var blogPost = new BlogPost { BlogPostId=4,Title="New Blog Post",Content="Content for new blog post",CreatedAt=DateTime.Now };
            blogPostRepositoryMock.Setup(repo => repo.CreateBlogPost(blogPost)).Returns(blogPost);

            // Act
            var result= blogPostService.CreateBlogPost(blogPost);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<BlogPost>(result);
            blogPostRepositoryMock.Verify(repo => repo.CreateBlogPost(blogPost), Times.Once);
        }

        [Fact, TestPriority(2)]
        public void GetAllBlogPosts_ReturnsAllBlogPosts()
        {
            // Arrange
           
            var expectedBlogPosts = new List<BlogPost>
            {
            new BlogPost { BlogPostId = 1, Title = "Blog Post 1", Content = "Content for blog post 1" },
            new BlogPost { BlogPostId = 2, Title = "Blog Post 2", Content = "Content for blog post 2" }
            };
            blogPostRepositoryMock.Setup(repo => repo.GetAllBlogPosts()).Returns(expectedBlogPosts);
       
            // Act
            var result = blogPostService.GetAllBlogPosts();

            // Assert
            Assert.IsAssignableFrom<List<BlogPost>>(result);
            Assert.Equal(expectedBlogPosts, result);
            Assert.Equal(2,expectedBlogPosts.Count);

        }

        [Fact, TestPriority(3)]
        public void GetBlogPostById_ReturnsBlogPostWithValidId()
        {
            // Arrange
            int id = 1;
            var expectedBlogPost = new BlogPost { BlogPostId = id, Title = "Blog Post 1", Content = "Content for blog post 1" ,CreatedAt=DateTime.Now};

            blogPostRepositoryMock.Setup(repo => repo.GetBlogPostById(id)).Returns(expectedBlogPost);
            

            // Act
            var result = blogPostService.GetBlogPostById(id);

            // Assert
            Assert.IsAssignableFrom<BlogPost>(result);
            Assert.Equal(expectedBlogPost, result);
            Assert.Equal("Blog Post 1", result.Title);
        }

        [Fact, TestPriority(4)]
        public void SearchBlogPostByUser_ReturnsBlogPostWithValidUser()
        {
            // Arrange
            int id = 1;
            var blogposts = new List<BlogPost>
            {
                new BlogPost { BlogPostId = 1, Title = "Test Post 1", Content = "Content for post 1", CreatedAt = DateTime.Now, Tags = new List<string> { "Post1 Tag1", "Post1 Tag2" }, AuthorId = 1, Author = new User { UserId = 1, UserName = "user1", Email = "User1@test.com", Password = "Password123" } },
                new BlogPost { BlogPostId = 2, Title = "Test Post 2", Content = "Content for post 2", CreatedAt = DateTime.Now, Tags = new List<string> { "Post1 Tag1", "Post1 Tag2" }, AuthorId = 1, Author = new User { UserId = 1, UserName = "user1", Email = "User1@test.com", Password = "Password123" } }
            };

            searchRepositoryMock.Setup(repo => repo.SearchBlogPostsByUser(id)).Returns(blogposts);


            // Act
            var result = searchService.SearchBlogPostsByUser(id);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<BlogPost>>(result);
            Assert.Equal(blogposts, result);
            Assert.Equal(2, blogposts.Count);
        }

        [Fact, TestPriority(4)]
        public void UpdateBlogPost_UpdateBlogPostSuccess()
        {
            // Arrange
            int id = 1;
            var blogPost = new BlogPost { BlogPostId = id, Title = "Updated Title Blog Post 1", Content = "Updated Content Blog Post 1",CreatedAt=DateTime.Now };

            blogPostRepositoryMock.Setup(repo => repo.UpdateBlogPost(id, blogPost)).Returns(true);
            

            // Act
            var result = blogPostService.UpdateBlogPost(id, blogPost);

            // Assert
            Assert.True(result);
            blogPostRepositoryMock.Verify(repo => repo.UpdateBlogPost(id, blogPost), Times.Once);
        }

        [Fact, TestPriority(5)]
        public void DeleteBlogPost_ReturnsTrueOnSuccess()
        {
            // Arrange
            int id = 1;          
            blogPostRepositoryMock.Setup(repo => repo.DeleteBlogPost(id)).Returns(true);
            
            // Act
            var result = blogPostService.DeleteBlogPost(id);

            // Assert
            Assert.True(result);
            blogPostRepositoryMock.Verify(repo => repo.DeleteBlogPost(id), Times.Once);
        }
        #endregion BlogServiceTests

    }
}
