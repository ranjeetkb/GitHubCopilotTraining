using BlogPostService.Models;
using BlogPostService.Service;
using Microsoft.AspNetCore.Mvc;


namespace SearchService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<BlogPost>> SearchBlogPostsByUser(int userId)
        {
            try
            {
                var blogPosts = _searchService.SearchBlogPostsByUser(userId);
                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the blog post: {ex.Message}");
            }
        }

        [HttpPost("tags")]
        public ActionResult<IEnumerable<BlogPost>> SearchBlogPostsByTags(IEnumerable<string> tags)
        {
            try
            {
                var blogPosts = _searchService.SearchBlogPostsByTags(tags);
                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the blog post: {ex.Message}");
            }
        }

        [HttpGet("title/{title}")]
        public ActionResult<IEnumerable<BlogPost>> SearchBlogPostsByTitle(string title)
        {
            try
            {
                var blogPosts = _searchService.SearchBlogPostsByTitle(title);
                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the blog post: {ex.Message}");
            }
        }
    }
}
