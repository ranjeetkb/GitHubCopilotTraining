using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
       
        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository)); ;
        }
       
        // GET: api/users
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userRepository.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving users: {ex.Message}");
            }
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _userRepository.GetById(id);

                if (user == null)
                {
                    return NotFound($"User with user id {id} not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the user: {ex.Message}");
            }
        }

        // POST: api/users
        [HttpPost]
        public IActionResult PostUser(User user)
        {
            try
            {
                _userRepository.Create(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the user: {ex.Message}");
            }
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            try
            {
                if (id != user.UserId)
                {
                    return BadRequest();
                }

                _userRepository.Update(user);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the user: {ex.Message}");
            }
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var existingUser = _userRepository.GetById(id);

                if (existingUser == null)
                {
                    return NotFound($"User with user id {id} not found.");
                }

                _userRepository.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting user: {ex.Message}");
            }
        }
    }
}
