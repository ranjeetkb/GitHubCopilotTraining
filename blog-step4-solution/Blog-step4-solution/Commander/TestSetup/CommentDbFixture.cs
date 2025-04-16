using CommentService.Models;
using CommentService.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;

namespace Commander.TestSetup
{
    public class CommentDbFixture:IDisposable
    {                           
        public CommentDbSettings context;
        public CommentRepository commentRepository;
        private IConfigurationRoot config;
        public CommentDbFixture()
        {
            config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"MongoDB:ConnectionString", "mongodb://localhost:27017"},
                    {"MongoDB:DatabaseName", "TestDB"},
                    {"MongoDB:CommentCollectionName", "TestComments"}
                }).Build();

            var mockCollection = new Mock<IMongoCollection<Comment>>();
            var mockSettings = new Mock<IOptions<CommentDbSettings>>();

            mockSettings.Setup(x => x.Value)
                .Returns(new CommentDbSettings
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "TestDB",
                    CommentCollectionName = "TestComments",
                });          
                   

            mockCollection.Setup(x => x.InsertOne(It.IsAny<Comment>(), null, default));
            mockCollection.Setup(x => x.ReplaceOne(
                It.IsAny<FilterDefinition<Comment>>(),
                It.IsAny<Comment>(),
                It.IsAny<ReplaceOptions>(),
                default))
            .Returns(new ReplaceOneResult.Acknowledged(1, 1, null));

            mockCollection.Setup(x => x.DeleteOne(
               FilterDefinition<Comment>.Empty, null, default))
               .Returns(new DeleteResult.Acknowledged(1));

            context = new CommentDbSettings(mockSettings.Object);
            commentRepository = new CommentRepository(context);

            context.commentCollection.DeleteMany(Builders<Comment>.Filter.Empty);
            context.commentCollection.InsertMany(new List<Comment>
                {
                new Comment{CommentId=1,BlogPostId=1,Content="First comment for blog post 1",CreatedAt=DateTime.Now.AddDays(-1)},
                new Comment{CommentId=2,BlogPostId=1,Content="Second comment for blog post 2",CreatedAt=DateTime.Now},
                new Comment{CommentId=3,BlogPostId=2,Content="First comment for blog post 3",CreatedAt=DateTime.Now},
                });
        }
    
        public void Dispose() {
            context = null;
        }
    }
}
