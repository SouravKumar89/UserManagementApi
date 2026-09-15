using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Models;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> Users = new()
        {
            new User
            {
                Id = 1111,
                Name = "Alice",
                Email = "alice@example.com",
                Age = 30
            },
            new User
            {
                Id = 1112,
                Name = "Bob",
                Email = "bob@example.com",
                Age = 35
            },
            new User
            {
                Id = 1113,
                Name = "Sourav",
                Email = "sourav@example.com",
                Age = 32
            },
            new User
            {
                Id = 1114,
                Name = "Kumar",
                Email = "kumar@example.com",
                Age = 25
            }
        };

        // GET: api/users
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(Users);
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            user.Id = Users.Count == 0
                ? 1
                : Users.Max(u => u.Id) + 1;

            Users.Add(user);

            return CreatedAtAction(
                nameof(GetUser),
                new { id = user.Id },
                user);
        }

        // PUT: api/users/1
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Age = updatedUser.Age;

            return Ok(user);
        }

        // DELETE: api/users/1
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            Users.Remove(user);

            return Ok(new
            {
                message = "User deleted successfully"
            });
        }
    }
}
