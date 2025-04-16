using BlogPostService.Models;
using BlogPostService.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using UserService.Models;

namespace Commander.TestSetup
{
    public class BlogPostDbFixture:IDisposable
    {                           
        public BlogDbSettings context;
        public BlogPostRepository blogPostRepository;
        private Mock<IMongoCollection<BlogPost>> mockCollection;
        private IConfigurationRoot config;
       
        public BlogPostDbFixture()
        {

            config = new ConfigurationBuilder()
               .AddInMemoryCollection(new Dictionary<string, string>
               {
                    {"MongoDB:ConnectionString", "mongodb://localhost:27017"},
                    {"MongoDB:DatabaseName", "TestDB"},
                    {"MongoDB:BlogPostCollectionName", "TestBlogPosts"}
               }).Build();

            
            var mockSettings = new Mock<IOptions<BlogDbSettings>>();

            mockSettings.Setup(x => x.Value)
                .Returns(new BlogDbSettings
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "TestDB",
                    BlogPostCollectionName = "TestBlogPosts",
                });

            mockCollection = new Mock<IMongoCollection<BlogPost>>();            

            mockCollection.Setup(x => x.InsertOne(It.IsAny<BlogPost>(), null, default));
            mockCollection.Setup(x => x.ReplaceOne(
                It.IsAny<FilterDefinition<BlogPost>>(),
                It.IsAny<BlogPost>(),
                It.IsAny<ReplaceOptions>(),
                default))
            .Returns(new ReplaceOneResult.Acknowledged(1, 1, null));

            mockCollection.Setup(x => x.DeleteOne(
               FilterDefinition<BlogPost>.Empty, null, default))
               .Returns(new DeleteResult.Acknowledged(1));

            context = new BlogDbSettings(mockSettings.Object);
            blogPostRepository = new BlogPostRepository(context);

            context.blogPostsCollection.DeleteMany(Builders<BlogPost>.Filter.Empty);
            context.blogPostsCollection.InsertMany(new List<BlogPost>
                {
                new BlogPost{ BlogPostId = 1, Title = "Test Post 1", Content = "Content for post 1", CreatedAt = DateTime.Now, Tags = new List<string> { "Post1 Tag1","Post1 Tag2" }, AuthorId = 1, Author = new User { UserId = 1, UserName = "user1", Email = "User1@test.com", Password = "Password123" } },
                new BlogPost{ BlogPostId = 2, Title = "Test Post 2", Content = "Content for post 2", CreatedAt = DateTime.Now, Tags = new List<string> { "Post2 Tag1" }, AuthorId = 1, Author = new User { UserId = 1, UserName = "user1", Email = "User1@test.com", Password = "Password123" } },
                new BlogPost{ BlogPostId = 3, Title = "Test Post 3", Content = "Content for post 3", CreatedAt = DateTime.Now, Tags = new List<string> { "Post3 Tag1", "Post3 Tag2"}, AuthorId = 2, Author = new User { UserId = 2, UserName = "user2", Email = "User2@test.com", Password = "Password123" } },
                new BlogPost{ BlogPostId = 4, Title = "Test Post 4", Content = "Content for post 4", CreatedAt = DateTime.Now, AuthorId = 2, Author = new User { UserId = 2, UserName = "user2", Email = "User2@test.com", Password = "Password123" } }
                });
           
        }
    
        public void Dispose() {
            context = null;
        }
    }
}
