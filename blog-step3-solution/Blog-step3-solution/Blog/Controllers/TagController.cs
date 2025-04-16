using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagRepository _tagRepository;

        public TagController(TagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpPost]
        public ActionResult<Tag> CreateTag(int id,string name)
        {
            try
            {
                var tag = _tagRepository.CreateTag(id, name);
                return Ok(tag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating a tag: {ex.Message}");
            }
        }

        [HttpGet("{tagId}")]
        public ActionResult<Tag> GetTagById(int tagId)
        {
            try
            {
                var tag = _tagRepository.GetTagById(tagId);
                if (tag == null)
                    return NotFound();

                return Ok(tag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving tag by Id: {ex.Message}");
            }
        }

        [HttpGet("name/{name}")]
        public ActionResult<List<Tag>> GetTagsByName(string name)
        {
            try
            {
                var tags = _tagRepository.GetTagsByName(name);
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving tags by name: {ex.Message}");
            }
        }

        [HttpPut("{tagId}")]
        public IActionResult UpdateTag(int tagId, [FromBody] string newName)
        {
            try
            {
                _tagRepository.UpdateTag(tagId, newName);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating tag: {ex.Message}");
            }
        }

        [HttpDelete("{tagId}")]
        public IActionResult DeleteTag(int tagId)
        {
            try
            {
                _tagRepository.DeleteTag(tagId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting a tag: {ex.Message}");
            }
        }
    }
}
