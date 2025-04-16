using BlogPostService.Models;

namespace BlogPostService.Service
{
    public interface ISearchService
    {
        IEnumerable<BlogPost> SearchBlogPostsByUser(int userId);
        IEnumerable<BlogPost> SearchBlogPostsByTags(IEnumerable<string> tags);
        IEnumerable<BlogPost> SearchBlogPostsByTitle(string title);
    }
}
