using BlogPostService.Models;
using BlogPostService.Service;
using Microsoft.AspNetCore.Mvc;


namespace BlogPostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BlogPost>> GetAllBlogPosts()
        {
            try
            {
                var blogPosts = _blogPostService.GetAllBlogPosts();
                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving blog posts: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public ActionResult<BlogPost> GetBlogPostById(int id)
        {
            try
            {
                var blogPost = _blogPostService.GetBlogPostById(id);
                if (blogPost == null)
                {
                    return NotFound();
                }
                return Ok(blogPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the blog post: {ex.Message}");
            }
        }
       

        [HttpPost]
        public IActionResult CreateBlogPost(BlogPost blogPost)
        {
            try
            {
                _blogPostService.CreateBlogPost(blogPost);
                return CreatedAtAction(nameof(GetBlogPostById), new { id = blogPost.BlogPostId }, blogPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the blog post: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogPost(int id, BlogPost blogPost)
        {
            try
            {
                var result = _blogPostService.UpdateBlogPost(id, blogPost);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the blog post: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogPost(int id)
        {
            try
            {
                var result = _blogPostService.DeleteBlogPost(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the blog post: {ex.Message}");
            }
        }
    }
}
