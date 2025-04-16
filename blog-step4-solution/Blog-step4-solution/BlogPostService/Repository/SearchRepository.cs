using BlogPostService.Models;
using MongoDB.Driver;


namespace BlogPostService.Repository
{
    public class SearchRepository:ISearchRepository
    {
        private readonly IMongoCollection<BlogPost> _blogPostsCollection;
        private BlogDbSettings _settings;
        public SearchRepository(BlogDbSettings settings)
        {
            _settings = settings;
            _blogPostsCollection = _settings.blogPostsCollection;
        }

        public IEnumerable<BlogPost> SearchBlogPostsByUser(int userId)
        {           
            return _blogPostsCollection.Find(blogPost => blogPost.AuthorId == userId).ToList();
        }

        public IEnumerable<BlogPost> SearchBlogPostsByTags(IEnumerable<string> tags)
        {
            var filter = Builders<BlogPost>.Filter.AnyIn(bp => bp.Tags, tags);
            return _blogPostsCollection.Find(filter).ToList();
        }

        public IEnumerable<BlogPost> SearchBlogPostsByTitle(string title)
        {
            var filter = Builders<BlogPost>.Filter.Regex(bp => bp.Title, new MongoDB.Bson.BsonRegularExpression(title, "i"));
            return _blogPostsCollection.Find(filter).ToList();
        }
    }
}
