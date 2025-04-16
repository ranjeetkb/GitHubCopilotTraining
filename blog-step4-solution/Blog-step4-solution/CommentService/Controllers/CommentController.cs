using CommentService.Models;
using CommentService.Service;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Comment>> GetAllComments()
        {
            try
            {
                var comments = _commentService.GetAllComments();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the comment: {ex.Message}");
            }
        }

        [HttpGet("blogpost/{blogPostId}")]
        public ActionResult<IEnumerable<Comment>> GetCommentsByBlogPostId(int blogPostId)
        {
            try
            {
                var comments = _commentService.GetCommentsByBlogPostId(blogPostId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the comment: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> GetCommentById(int id)
        {
            try
            {
                var comment = _commentService.GetCommentById(id);
                if (comment == null)
                {
                    return NotFound();
                }
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the comment: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateComment(Comment comment)
        {
            try
            {
                _commentService.CreateComment(comment);
                return CreatedAtAction(nameof(GetCommentById), new { id = comment.CommentId }, comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the comment: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, Comment comment)
        {
            try
            {
                var result = _commentService.UpdateComment(id, comment);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the comment: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            try
            {
                var result = _commentService.DeleteComment(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the comment: {ex.Message}");
            }
        }
    }
}
