using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BlogPostService.Models
{
    public class BlogDbSettings
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string BlogPostCollectionName { get; set; } = null;

        public IMongoCollection<BlogPost> blogPostsCollection;

        public BlogDbSettings() { }
        public BlogDbSettings(IOptions<BlogDbSettings> blogcontext)
        {
            var mongoClient = new MongoClient(
              blogcontext.Value.ConnectionString
              );

            var mongoDatabase = mongoClient.GetDatabase(
                blogcontext.Value.DatabaseName
                );

            blogPostsCollection = mongoDatabase.GetCollection<BlogPost>(
                blogcontext.Value.BlogPostCollectionName
                );
        }

    }        
    
}
