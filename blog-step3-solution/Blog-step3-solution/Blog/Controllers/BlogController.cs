using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Entities;
using CustomExceptions;

namespace Blog.Controllers
{
    // API BlogController for handling the HTTP methods.    

    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
       
        private readonly IRepository<BlogPost> _blogRepository;  

       
        public BlogController(IRepository<BlogPost> blogRepository)
        {
            _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
        }       
       

        [HttpGet]
        public IActionResult GetAllBlogPosts()
        {
            try
            {
                var blogPosts = _blogRepository.GetAll();
                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving blog posts: {ex.Message}");
            }
        }

       
        [HttpGet("{id}")]
        public IActionResult GetBlogPost(int id)
        {
            try
            {
                var blogPost = _blogRepository.GetById(id);
                if (blogPost == null)
                {
                    return NotFound($"Blog post with ID {id} not found.");
                    
                }
                return Ok(blogPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the blog post: {ex.Message}");
            }
        }

       
        [HttpPost]
        public IActionResult PostBlogPost(BlogPost blogPost)
        {
            try
            {
                _blogRepository.Create(blogPost);
                return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.Id }, blogPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the blog post: {ex.Message}");
            }
        }

       
        [HttpPut("{id}")]
        public IActionResult PutBlogPost(int id, BlogPost blogPost)
        {
            try
            {
                if (id != blogPost.Id)
                {
                    return BadRequest("Blog post ID mismatch.");
                }

                _blogRepository.Update(blogPost);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the blog post: {ex.Message}");
            }
        }

      
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogPost(int id)
        {
            try
            {
                var existingBlogPost = _blogRepository.GetById(id);
                if (existingBlogPost == null)
                {
                    return NotFound($"Blog post with ID {id} not found.");
                    
                }

                _blogRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the blog post: {ex.Message}");
            }
        }
    }
}
