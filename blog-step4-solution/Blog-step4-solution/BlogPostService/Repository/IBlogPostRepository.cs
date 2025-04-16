using BlogPostService.Models;
namespace BlogPostService.Repository
{
    public interface IBlogPostRepository
    {
        IEnumerable<BlogPost> GetAllBlogPosts();
        BlogPost GetBlogPostById(int id);        
        BlogPost CreateBlogPost(BlogPost blogpost);
        bool UpdateBlogPost(int id, BlogPost blogpost);
        bool DeleteBlogPost(int id);       
    }
}
