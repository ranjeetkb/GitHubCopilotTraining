using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _repository;
       
        public CategoryController(IRepository<Category> repository)
        {
            _repository = repository;
        }
       
        // GET: api/categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _repository.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving categories: {ex.Message}");
            }
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                var category = _repository.GetById(id);

                if (category == null)
                {
                    return NotFound($"Category with category id {id} not found.");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the category: {ex.Message}");
            }
        }

        // POST: api/categories
        [HttpPost]
        public IActionResult PostCategory(Category category)
        {
            try
            {
                _repository.Create(category);
                return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the category: {ex.Message}");
            }
        }

        // PUT: api/categories/5
        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, Category category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest("Category ID mismatch.");
                }

                _repository.Update(category);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the category: {ex.Message}");
            }
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var existingCategory = _repository.GetById(id);

                if (existingCategory == null)
                {
                    return NotFound($"Category with category id {id} not found.");
                }

                _repository.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the category: {ex.Message}");
            }
        }
    }
}
