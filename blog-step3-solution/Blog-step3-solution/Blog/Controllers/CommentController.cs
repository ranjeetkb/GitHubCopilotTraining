using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
       
    public class CommentController : ControllerBase
    {
        private readonly CommentRepository _commentRepository;

        public CommentController(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpPost]
        public ActionResult<Comment> CreateComment([FromBody] Comment commentobj)
        {
            try
            {
                var comment = _commentRepository.CreateComment(commentobj);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the comment: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> GetCommentById(int id)
        {
            try
            {
                var comment = _commentRepository.GetCommentById(id);
                if (comment == null)
                    return NotFound();

                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the comment: {ex.Message}");
            }
        }

        [HttpGet("post/{postId}")]
        public ActionResult<List<Comment>> GetCommentsForPost(int postId)
        {
            try
            {
                var comments = _commentRepository.GetCommentsForPost(postId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the comment for post: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, Comment comm)
        {
            try
            {
                _commentRepository.UpdateComment(id, comm.Content);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the comment: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            try
            {
                _commentRepository.DeleteComment(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the comment: {ex.Message}");
            }
        }
    }
}
