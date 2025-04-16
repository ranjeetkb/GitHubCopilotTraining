using BlogPostService.Models;
namespace BlogPostService.Repository
{
    public interface ISearchRepository
    {
        IEnumerable<BlogPost> SearchBlogPostsByUser(int userId);
        IEnumerable<BlogPost> SearchBlogPostsByTags(IEnumerable<string> tags);
        IEnumerable<BlogPost> SearchBlogPostsByTitle(string title);
    }
}
