using BlogPostService.Models;
using BlogPostService.Repository;
using CommentService.Models;
using CommentService.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using UserService.Models;
using UserService.Repository;

namespace Commander.TestSetup
{
    public class BlogControllerFixture : IDisposable
    {
        public BlogDbSettings blogcontext;
        public BlogPostRepository blogPostRepository;
        private Mock<IMongoCollection<BlogPost>> mockBlogPostCollection;

        public CommentDbSettings commentcontext;
        public CommentRepository commentRepository;
        private Mock<IMongoCollection<Comment>> mockCommentCollection;

        private IConfigurationRoot config;

        public BlogControllerFixture()
        {

            config = new ConfigurationBuilder()
               .AddInMemoryCollection(new Dictionary<string, string>
               {
                    {"MongoDB:ConnectionString", "mongodb://localhost:27017"},
                    {"MongoDB:DatabaseName", "TestControllerDB"},
                    {"MongoDB:BlogPostCollectionName", "TestBlogPosts"}
               }).Build();

            //BlogPost Settings

            var mockBlogPostSettings = new Mock<IOptions<BlogDbSettings>>();

            mockBlogPostSettings.Setup(x => x.Value)
                .Returns(new BlogDbSettings
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "TestControllerDB",
                    BlogPostCollectionName = "TestBlogPosts"

                });

            mockBlogPostCollection = new Mock<IMongoCollection<BlogPost>>();

            blogcontext = new BlogDbSettings(mockBlogPostSettings.Object);
            blogPostRepository = new BlogPostRepository(blogcontext);

            blogcontext.blogPostsCollection.DeleteMany(Builders<BlogPost>.Filter.Empty);
            blogcontext.blogPostsCollection.InsertMany(new List<BlogPost>
                {
                new BlogPost{ BlogPostId = 1, Title = "Test Post 1", Content = "Content for post 1", CreatedAt = DateTime.Now, Tags = new List<string> { "Post1 Tag1","Post1 Tag2" }, AuthorId = 1, Author = new User { UserId = 1, UserName = "user1", Email = "User1@test.com", Password = "Password123" } },
                new BlogPost{ BlogPostId = 2, Title = "Test Post 2", Content = "Content for post 2", CreatedAt = DateTime.Now, Tags = new List<string> { "Post2 Tag1" }, AuthorId = 1, Author = new User { UserId = 1, UserName = "user1", Email = "User1@test.com", Password = "Password123" } },
                new BlogPost{ BlogPostId = 3, Title = "Test Post 3", Content = "Content for post 3", CreatedAt = DateTime.Now, Tags = new List<string> { "Post3 Tag1", "Post3 Tag2"}, AuthorId = 2, Author = new User { UserId = 2, UserName = "user2", Email = "User2@test.com", Password = "Password123" } },
                new BlogPost{ BlogPostId = 4, Title = "Test Post 4", Content = "Content for post 4", CreatedAt = DateTime.Now, AuthorId = 2, Author = new User { UserId = 2, UserName = "user2", Email = "User2@test.com", Password = "Password123" } }
                });

        }

        public void Dispose()
        {
            blogcontext = null;

        }
    }

    public class CommentControllerFixture : IDisposable
    {

        public CommentDbSettings commentcontext;
        public CommentRepository commentRepository;
        private Mock<IMongoCollection<Comment>> mockCommentCollection;

        private IConfigurationRoot config;

        public CommentControllerFixture()
        {

            config = new ConfigurationBuilder()
               .AddInMemoryCollection(new Dictionary<string, string>
               {
                    {"MongoDB:ConnectionString", "mongodb://localhost:27017"},
                    {"MongoDB:DatabaseName", "TestControllerDB"},
                    {"MongoDB:CommentCollectionName", "TestComments"}
               }).Build();


            //Comment Settings

            var mockCommentSettings = new Mock<IOptions<CommentDbSettings>>();

            mockCommentSettings.Setup(x => x.Value)
                .Returns(new CommentDbSettings
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "TestControllerDB",
                    CommentCollectionName = "TestComments",
                });

            mockCommentCollection = new Mock<IMongoCollection<Comment>>();

            commentcontext = new CommentDbSettings(mockCommentSettings.Object);
            commentRepository = new CommentRepository(commentcontext);

            commentcontext.commentCollection.DeleteMany(Builders<Comment>.Filter.Empty);
            commentcontext.commentCollection.InsertMany(new List<Comment>
            {
                new Comment{CommentId=1,BlogPostId=1,Content="First comment for blog post 1",CreatedAt=DateTime.Now.AddDays(-1)},
                new Comment{CommentId=2,BlogPostId=1,Content="Second comment for blog post 1",CreatedAt=DateTime.Now},
                new Comment{CommentId=3,BlogPostId=2,Content="First comment for blog post 2",CreatedAt=DateTime.Now},
             });

        }

        public void Dispose()
        {
            commentcontext = null;

        }
    }

    public class UserControllerFixture : IDisposable
    {

        public UserDbSettings usercontext;
        public UserRepository userRepository;
        private Mock<IMongoCollection<User>> mockUserCollection;

        private IConfigurationRoot config;

        public UserControllerFixture()
        {

            config = new ConfigurationBuilder()
               .AddInMemoryCollection(new Dictionary<string, string>
               {
                    {"MongoDB:ConnectionString", "mongodb://localhost:27017"},
                    {"MongoDB:DatabaseName", "TestControllerDB"},
                    {"MongoDB:UserCollectionName", "TestUsers"}
               }).Build();


            //User Settings

            var mockUserSettings = new Mock<IOptions<UserDbSettings>>();

            mockUserSettings.Setup(x => x.Value)
                .Returns(new UserDbSettings
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "TestControllerDB",
                    UserCollectionName = "TestUsers",
                });

            mockUserCollection = new Mock<IMongoCollection<User>>();

            usercontext = new UserDbSettings(mockUserSettings.Object);
            userRepository = new UserRepository(usercontext);

            usercontext.usersCollection.DeleteMany(Builders<User>.Filter.Empty);
            usercontext.usersCollection.InsertMany(new List<User>
            {
                new User{UserId=1,UserName="User1",Email="user1@gmail.com",Password="Password123"},
                new User{UserId=2,UserName="User2",Email="user2@gmail.com",Password="Password123"}
             });

        }

        public void Dispose()
        {
            usercontext = null;

        }
    }
}
