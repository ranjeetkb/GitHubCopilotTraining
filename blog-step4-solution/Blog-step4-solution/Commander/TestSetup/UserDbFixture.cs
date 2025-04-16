using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using UserService.Models;
using UserService.Repository;

namespace Commander.TestSetup
{
    public class UserDbFixture:IDisposable
    {                           
        public UserDbSettings context;
        public UserRepository userRepository;
        private IConfigurationRoot config;
        public UserDbFixture()
        {
            config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"MongoDB:ConnectionString", "mongodb://localhost:27017"},
                    {"MongoDB:DatabaseName", "TestDB"},
                    {"MongoDB:UserCollectionName", "TestUsers"}
                }).Build();

            var mockCollection = new Mock<IMongoCollection<User>>();
            var mockSettings = new Mock<IOptions<UserDbSettings>>();

            mockSettings.Setup(x => x.Value)
                .Returns(new UserDbSettings
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "TestDB",
                    UserCollectionName = "TestUsers",
                });          
                   

            mockCollection.Setup(x => x.InsertOne(It.IsAny<User>(), null, default));
            mockCollection.Setup(x => x.ReplaceOne(
                It.IsAny<FilterDefinition<User>>(),
                It.IsAny<User>(),
                It.IsAny<ReplaceOptions>(),
                default))
            .Returns(new ReplaceOneResult.Acknowledged(1, 1, null));

            mockCollection.Setup(x => x.DeleteOne(
               FilterDefinition<User>.Empty, null, default))
               .Returns(new DeleteResult.Acknowledged(1));

            context = new UserDbSettings(mockSettings.Object);
            userRepository = new UserRepository(context);

            context.usersCollection.DeleteMany(Builders<User>.Filter.Empty);
            context.usersCollection.InsertMany(new List<User>
                {
                new User{UserId=1,UserName="User1",Email="user1@gmail.com",Password="Password123"},
                new User{UserId=2,UserName="User2",Email="user2@gmail.com",Password="Password123"}
                });
        }
    
        public void Dispose() {
            context = null;
        }
    }
}
