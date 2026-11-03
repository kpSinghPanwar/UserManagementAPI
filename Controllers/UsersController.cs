using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        
        [HttpGet("{id:int}")]
        public IActionResult GetUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Message = "Invalid user ID."
                });
            }

            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound(new
                {
                    Message = $"User with Id {id} was not found."
                });
            }

            return Ok(user);
        }
       
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
           
                var createdUser = _userService.AddUser(user);

                return CreatedAtAction(
                    nameof(GetUser),
                    new { id = createdUser.Id },
                    createdUser);        
           

        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (user.Id != 0 && user.Id != id)
                {
                    return BadRequest(new
                    {
                        Message = "Route ID and User ID do not match."
                    });
                }


            var updated = _userService.UpdateUser(id, user);

            if (!updated)
            {
                return NotFound(new
                {
                    Message = $"User with Id {id} was not found."
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Message = "Invalid user ID."
                });
            }

            var deleted = _userService.DeleteUser(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    Message = $"User with Id {id} was not found."
                });
            }

            return NoContent();
        }
    }
}