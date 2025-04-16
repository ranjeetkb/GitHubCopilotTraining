using BlogPostService.Models;
using BlogPostService.Repository;

namespace BlogPostService.Service
{
    public class SearchService:ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public IEnumerable<BlogPost> SearchBlogPostsByUser(int userId)
        {
            return  _searchRepository.SearchBlogPostsByUser(userId);
        }

        public IEnumerable<BlogPost> SearchBlogPostsByTags(IEnumerable<string> tags)
        {
            return _searchRepository.SearchBlogPostsByTags(tags);
        }

        public IEnumerable<BlogPost> SearchBlogPostsByTitle(string title)
        {
            return _searchRepository.SearchBlogPostsByTitle(title);
        }
    }
}
