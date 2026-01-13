using LTADotNetTrainingBatch3.Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LTADotNetTrainingBatch3.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        AppDbContext dbContext = new AppDbContext();

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetProduct(int pageNo, int pageSize)
        {    
            
            var products = dbContext.TblProducts
                .Where(x=> x.DeleteFlag == false)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.Quantity,
                    p.Price
                })
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();



            return Ok(products);
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(string productName, int qty, decimal price)
        {
            if(string.IsNullOrEmpty(productName) || qty < 0 || price < 0)
            {
                return BadRequest("Invalid product data.");
            }
            TblProduct newProduct = new TblProduct
            {
                ProductName = productName,
                Quantity = qty,
                Price = price,
                DeleteFlag = false,
                CreatedDateTime = DateTime.Now,
                ModifiedDateTime = DateTime.Now
            };
            dbContext.TblProducts.Add(newProduct);
            dbContext.SaveChanges();
            return Ok("Product added successfully.");
        }

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(int productId, string productName, int qty, decimal price)
        {
            var existingProduct = dbContext.TblProducts.FirstOrDefault(p => p.ProductId == productId);
            if (existingProduct != null)
            {
                existingProduct.ProductName = productName;
                existingProduct.Quantity = qty;
                existingProduct.Price = price;
                existingProduct.ModifiedDateTime = DateTime.Now;

                int result = dbContext.SaveChanges();
                string message = result > 0 ? "Update successful" : "Update failed";
                return Ok(message);
            }
            else
            {
               
               return NotFound("Product not found.");
            }
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int productID)
        {
            var existingProduct = dbContext.TblProducts.FirstOrDefault(p => p.ProductId == productID);
            if (existingProduct != null)
            {

                dbContext.TblProducts.Remove(existingProduct);
                int result = dbContext.SaveChanges();
                string message = result > 0 ? "Delete successful" : "Update failed";
                return Ok(message);
            }
            else
            {

                return NotFound("Product not found.");
            }
        }

     
    }
}
