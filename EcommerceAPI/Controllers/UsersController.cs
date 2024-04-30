using Microsoft.AspNetCore.Http;
using EcommerceAPI.Models;
using EcommerceAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
               _context = context; 
        }


        /*
* This method handles the HTTP POST request to register a new user.
* It checks if the user's email is already in use, adds the user to the database if not, and returns the newly created user.
*/

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            // Check if the user's email is already in use
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                // Return a bad request response with a message if email is already in use
                return BadRequest("Email already in use.");
            }

            // Add the user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Return a response with the newly created user
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        /*
        * This method handles the HTTP GET request to retrieve a user by ID.
        * It finds the user in the database based on the provided ID and returns the user if found.
        */

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            // Find the user in the database based on the ID
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                // Return a not found response if user is not found
                return NotFound();
            }
            return user;
        }


               /*
        * This method handles the HTTP POST request for user login.
        */

        [HttpPost("login")] // Specifies the HTTP POST route for login
        public async Task<ActionResult<User>> Login(string email, string password) // Method signature with async Task that returns User based on email and password
        {
            var user = await _context.Users // Retrieve user data from the database
                            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password); // Query to find a user with the provided email and password

            if (user == null) // Check if user is not found
            {
                return Unauthorized("Invalid credentials"); // Return unauthorized status with a message
            }

            return user; // Return the user if found
        }



               /*
        * This method handles the HTTP PUT request to update a specific user by ID.
        */

        [HttpPut("update/{id}")] // Specifies the HTTP PUT route for updating a user with a specific ID
        public async Task<IActionResult> UpdateUser(int id, User updatedUser) // Method signature with async Task that updates a user based on ID and updated user object
        {
            if (id != updatedUser.UserId) // Check if the provided ID does not match the user's ID
            {
                return BadRequest(); // Return a bad request response
            }

            _context.Entry(updatedUser).State = EntityState.Modified; // Set the state of the updated user entity to modified

            try
            {
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(u => u.UserId == id)) // Check if the user with the provided ID does not exist
                {
                    return NotFound(); // Return a not found response
                }
                else
                {
                    throw; // Throw an exception for database update concurrency issues
                }
            }

            return NoContent(); // Return no content response upon successful update
        }



    }
}
