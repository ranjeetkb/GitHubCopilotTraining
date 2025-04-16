using BlogPostService.Exceptions;
using BlogPostService.Models;
using BlogPostService.Repository;
using Commander.TestSetup;
using MongoDB.Driver;
using UserService.Models;

namespace RepositoryTests
{
    [TestCaseOrderer("TestSetup.PriorityOrderer", "commander")]
    public class BlogPostRepositoryTest:IClassFixture<BlogPostDbFixture>
    {
        private IBlogPostRepository blogPostRepository;
        private ISearchRepository searchRepository;
        private IMongoCollection<BlogPost> blogpostcollection;

        public BlogPostRepositoryTest(BlogPostDbFixture blogPostDbFixture)
        {
            blogPostRepository = new BlogPostRepository(blogPostDbFixture.context);
            searchRepository = new SearchRepository(blogPostDbFixture.context);
            blogpostcollection = blogPostDbFixture.context.blogPostsCollection;
        }

        #region Positive BlogRepositoryTests    

        [Fact,TestPriority(1)]
        public void CreateBlogPost_ShouldCreateBlogPost()
        {
            //Arrange
            var blogpost5 = new BlogPost() { BlogPostId = 5, Title = "Test Post 5", Content = "Content for post 5", CreatedAt = DateTime.Now.AddDays(-1), AuthorId = 3, Author = new User { UserId = 3, UserName = "user3", Email = "User3@test.com", Password = "Password123" } };
            
            //Act
            var result=blogPostRepository.CreateBlogPost(blogpost5);

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<BlogPost>(result);
            Assert.Equal(5, result.BlogPostId);
            Assert.Equal("Test Post 5", result.Title);           

        }      

        [Fact,TestPriority(2)]
        public void GetAllBlogPosts_ReturnsAllBlogPosts()
        {
            // Act            
            var blogposts = blogPostRepository.GetAllBlogPosts();

            // Assert
            Assert.NotNull(blogposts);
            Assert.Equal(blogpostcollection.CountDocuments(FilterDefinition<BlogPost>.Empty), blogposts.Count());

        }

        [Fact,TestPriority(3)]
        public void GetBlogPostById_ReturnsBlogPostWithValidId()
        {
            // Arrange
            var Id = 4;

            // Act
            var blogpost = blogPostRepository.GetBlogPostById(Id);

            // Assert
            Assert.NotNull(blogpost);
            Assert.IsAssignableFrom<BlogPost>(blogpost);            
            Assert.Contains("Test Post 4", blogpost.Title);
            Assert.Contains("Content for post 4", blogpost.Content);
        }
      
        [Fact,TestPriority(4)]
        public void UpdateBlogPost_UpdateBlogPostSuccess()
        {
            //Arrange
            int Id = 1;
            var blogPostToUpdate = new BlogPost() { BlogPostId = 1, Title = "Updated Test Post 1", Content = "Updated content for post 1", Tags = new List<string> { "Post1 Tag1", "Post1 Tag2" }, AuthorId = 1, Author = new UserService.Models.User { UserId = 1, UserName = "user1", Email = "User1@test.com", Password = "Password123" } };

            // Act
            var result = blogPostRepository.UpdateBlogPost(id: Id, blogPostToUpdate);

            // Assert
            Assert.True(result);
            var updatedblogpost = blogpostcollection.Find(x => x.BlogPostId == Id).FirstOrDefault();
            Assert.Contains("Updated Test Post 1",updatedblogpost.Title);
            Assert.Contains("Updated content for post 1", updatedblogpost.Content);
        }

        [Fact,TestPriority(5)]
        public void DeleteBlogPost_ReturnsTrueOnSuccess()
        {
            //Arrange
            int Id = 3;

            // Act
            var result = blogPostRepository.DeleteBlogPost(Id);

            // Assert
            Assert.True(result);
            var deletedblogpost = blogpostcollection.Find(x => x.BlogPostId == Id).FirstOrDefault();
            Assert.Null(deletedblogpost);
        }
        #endregion Positive BlogRepositoryTests


        #region Positive SearchBlogPostTests

        [Fact, TestPriority(6)]
        public void SearchBlogPostsByUser_ShoudReturnBlogPost()
        {
            //Arrange
            var userid = 1;

            //Act
            var userblogposts = searchRepository.SearchBlogPostsByUser(userid);

            // Assert
            Assert.NotNull(userblogposts);            
            var findposts = blogpostcollection.Find(Builders<BlogPost>.Filter.Eq("AuthorId", userid));           
            Assert.Equal(findposts.CountDocuments(), userblogposts.Count());
        }

        [Fact, TestPriority(7)]
        public void SearchBlogPostsByTag_ShouldReturnBlogPosts()
        {
            // Act            
            var tags = new List<string> { "Post1 Tag1" };

            //Act
            var tagsblogposts = searchRepository.SearchBlogPostsByTags(tags);

            // Assert
            Assert.NotNull(tagsblogposts);
            Assert.Equal("Post1 Tag1", tagsblogposts.FirstOrDefault().Tags.FirstOrDefault());
        }               

        [Fact, TestPriority(9)]
        public void SearchBlogPostsByTitle_ShouldReturnBlogPosts()
        {
            // Act            
            var title = "Test Post 2";

            //Act
            var titleblogposts = searchRepository.SearchBlogPostsByTitle(title);

            // Assert
            Assert.NotNull(titleblogposts);
            Assert.Equal("Test Post 2", titleblogposts.FirstOrDefault().Title);

        }

        #endregion Positive SearchBlogPostTests

        #region Negative BlogRepositoryTests
        [Fact, TestPriority(10)]
        public void CreateBlogPostWithExistingId_ShouldThrowException()
        {
            // Arrange         
            //Arrange
            var newblogpost = new BlogPost() { BlogPostId = 1, Title = "New Blog Post", Content = "Content for new blog post", CreatedAt = DateTime.Now };

            // Act && Assert            
            Assert.Throws<BlogPostAlreadyExistsException>(() =>
            {
                blogPostRepository.CreateBlogPost(newblogpost);
            });

        }

        [Fact, TestPriority(11)]
        public void GetBlogPostByInvalidId_ReturnsException()
        {
            // Arrange
            var userId = 100;

            // Act && Assert
            Assert.Throws<BlogPostNotFoundException>(() =>
            {
                blogPostRepository.GetBlogPostById(userId);
            });
        }
        #endregion Negative BlogRepositoryTests

        #region Negative SearchRepositoryTests

        [Fact,TestPriority(12)]
        public void SearchBlogPostsByInvalidUser_ShoudReturnNoResult()
        {
            //Arrange
            var userid = 100;

            //Act
            var userblogposts = searchRepository.SearchBlogPostsByUser(userid);

            // Assert
            Assert.NotNull(userblogposts);          
            Assert.Equal(0,userblogposts.Count());
                       
        }

        [Fact, TestPriority(13)]
        public void SearchBlogPostsByInvalidTitle_ShouldReturnNoResult()
        {
            // Act            
            var title = "Some Title";

            //Act
            var titleblogposts = searchRepository.SearchBlogPostsByTitle(title);

            // Assert
            Assert.NotNull(titleblogposts);
            Assert.Equal(0, titleblogposts.Count());

        }
        #endregion Negative SearchRepositoryTests

    }
}
