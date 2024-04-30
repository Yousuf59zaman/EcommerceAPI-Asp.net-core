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
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*
        * This method handles the HTTP GET request to retrieve a product by ID.
        */

        [HttpGet("{id}")] // Specifies the HTTP GET route for retrieving a product by ID
        public async Task<ActionResult<Product>> GetProduct(int id) // Method signature with async Task that returns a product based on ID
        {
            var product = await _context.Products.FindAsync(id); // Retrieve product data from the database based on ID

            if (product == null) // Check if product is not found
            {
                return NotFound(); // Return a not found response
            }

            return product; // Return the product if found
        }


        /*
        * This method handles the HTTP GET request to retrieve all products.
        */

        [HttpGet] // Specifies the HTTP GET route for retrieving all products
        public async Task<ActionResult<List<Product>>> GetAllProducts() // Method signature with async Task that returns a list of products
        {
            var products = await _context.Products.ToListAsync(); // Retrieve all products from the database

            return products; // Return the list of products
        }



        // Add a new product
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id); // Retrieve the product by ID

            if (product == null) // Check if product is not found
            {
                return NotFound(); // Return a not found response
            }

            _context.Products.Remove(product); // Remove the product from the context
            await _context.SaveChangesAsync(); // Save the changes to the database

            return Ok("Deleted Product Successfully"); // Return a success response
        }

        /*
        * Update an existing product
        */

        // Specifies the HTTP PUT route for updating a product with a specific ID
        [HttpPut("{id}")]
        // Method signature with async Task that updates a product based on ID and updated product object
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            // Check if the provided ID does not match the product's ID
            if (id != product.ProductId)
            {
                return BadRequest(); // Return a bad request response
            }

            _context.Entry(product).State = EntityState.Modified; // Set the state of the updated product entity to modified

            try
            {
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(p => p.ProductId == id)) // Check if the product with the provided ID does not exist
                {
                    return NotFound(); // Return a not found response
                }
                else
                {
                    throw; // Throw an exception for database update concurrency issues
                }
            }

            return Ok("Updated Product Successfully"); // Return no content response upon successful update
        
        }




    }
}
