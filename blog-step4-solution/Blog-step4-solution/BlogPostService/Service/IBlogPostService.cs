using BlogPostService.Models;

namespace BlogPostService.Service
{
    public interface IBlogPostService
    {
        IEnumerable<BlogPost> GetAllBlogPosts();
        BlogPost GetBlogPostById(int id);
        BlogPost CreateBlogPost(BlogPost blogpost);
        bool UpdateBlogPost(int id, BlogPost blogpost);
        bool DeleteBlogPost(int id);
    }
}
