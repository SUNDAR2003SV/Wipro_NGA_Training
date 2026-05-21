using Microsoft.AspNetCore.Mvc;
using SmartInventory_API_RoutingDemo.Models;

namespace SmartInventory_API_RoutingDemo.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : Controller
    {
        static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 60000.00m, Quantity = 10 },
            new Product { Id = 2, Name = "Smartphone", Price = 25000.00m, Quantity = 20 },
            new Product { Id = 3, Name = "Headphones", Price = 199.99m, Quantity = 15 }
        };

        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            return Ok(products);
        }


        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]

        public ActionResult<Product> CreateProduct(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]

        public ActionResult UpdateProduct(int id, Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Quantity = updatedProduct.Quantity;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products.Remove(product);
            return NoContent();
        }
    }
}