using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CommentService.Models
{
    public class CommentDbSettings
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string CommentCollectionName { get; set; } = null;

        public IMongoCollection<Comment> commentCollection;

        public CommentDbSettings() { }
        public CommentDbSettings(IOptions<CommentDbSettings> commentcontext)
        {
            var mongoClient = new MongoClient(
                    commentcontext.Value.ConnectionString
                    );

            var mongoDatabase = mongoClient.GetDatabase(
                commentcontext.Value.DatabaseName
                );

            commentCollection = mongoDatabase.GetCollection<Comment>(
                commentcontext.Value.CommentCollectionName
            );
        }




    }
}
