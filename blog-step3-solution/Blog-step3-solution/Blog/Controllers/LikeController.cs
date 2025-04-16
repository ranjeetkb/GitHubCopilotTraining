using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Service;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly LikeRepository _likeRepository;

        public LikeController(LikeRepository repository)
        {
            _likeRepository = repository;
        }

        [HttpPost]
        public IActionResult CreateLike([FromBody] Like like)
        {
            try
            {
                _likeRepository.CreateLike(like.UserId, like.PostId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating like: {ex.Message}");
            }
        }

        [HttpGet("post/{postId}")]
        public ActionResult<List<Like>> GetLikesForPost(int postId)
        {
            try
            {
                var likesForPost = _likeRepository.GetLikesForPost(postId);
                return Ok(likesForPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving likes for post: {ex.Message}");
            }
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<Like>> GetLikesByUser(int userId)
        {
            try
            {
                var likesByUser = _likeRepository.GetLikesByUser(userId);
                return Ok(likesByUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving likes for a user: {ex.Message}");
            }
        }

        [HttpDelete("{userId}/{postId}")]
        public IActionResult DeleteLike(int userId, int postId)
        {
            try
            {
                _likeRepository.DeleteLike(userId, postId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting like: {ex.Message}");
            }
        }
    }
}
