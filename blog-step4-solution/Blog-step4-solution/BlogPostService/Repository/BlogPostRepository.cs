using BlogPostService.Exceptions;
using BlogPostService.Models;
using MongoDB.Driver;

namespace BlogPostService.Repository
{
    public class BlogPostRepository:IBlogPostRepository
    {
        private readonly IMongoCollection<BlogPost> _blogPostsCollection;
        private BlogDbSettings _settings;
        public BlogPostRepository(BlogDbSettings settings)
        {
            _settings = settings;
            _blogPostsCollection = _settings.blogPostsCollection;
        }

        public IEnumerable<BlogPost> GetAllBlogPosts()
        {
            return _blogPostsCollection.Find(_ => true).ToList();
        }

        public BlogPost GetBlogPostById(int id)
        {
            var blogPost = _blogPostsCollection.Find(post => post.BlogPostId == id).FirstOrDefault();
            if (blogPost == null)
                throw new BlogPostNotFoundException($"Blog post with id {id} not found");
            return blogPost;

        }
       
        public BlogPost CreateBlogPost(BlogPost blogpost)
        {
            try
            {
                _blogPostsCollection.InsertOne(blogpost);
                return blogpost;
            }
            catch (Exception) {
                throw new BlogPostAlreadyExistsException($"Blog post with id {blogpost.BlogPostId} already exixts");
            }
        }

        public bool UpdateBlogPost(int id, BlogPost blogpost)
        {
            var result = _blogPostsCollection.ReplaceOne(bp=>bp.BlogPostId == id, blogpost);
            if (result.ModifiedCount == 0)
                throw new BlogPostNotFoundException($"Blog post with id {id} not found");
            return result.IsAcknowledged && result.ModifiedCount>0;
            
            
        }

        public bool DeleteBlogPost(int id)
        {
           
             var result = _blogPostsCollection.DeleteOne(p => p.BlogPostId == id);
             if (result.DeletedCount == 0)
                    throw new BlogPostNotFoundException($"Blog post with id {id} not found");
             return result.IsAcknowledged && result.DeletedCount>0;
           
        }

        
    }
}
