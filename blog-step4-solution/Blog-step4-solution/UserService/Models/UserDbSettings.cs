using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UserService.Models
{
    public class UserDbSettings
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string UserCollectionName { get; set; } = null;

        public IMongoCollection<User> usersCollection;
        public UserDbSettings()
        {
                
        }
        public UserDbSettings(IOptions<UserDbSettings> usercontext)
        {

            var mongoClient = new MongoClient(
                   usercontext.Value.ConnectionString
                   ) ;

            var mongoDatabase = mongoClient.GetDatabase(
                usercontext.Value.DatabaseName
                );

            usersCollection = mongoDatabase.GetCollection<User>(
                usercontext.Value.UserCollectionName);
        }
        
        
    }
}
