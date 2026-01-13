using LTADotNetTrainingBatch3.Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTADotNetTrainingBatch3.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        AppDbContext dbContext = new AppDbContext();
        [HttpGet("{pageNo}, {pageSize}")]
        public IActionResult GetProductCategory(int pageNo, int pageSize)
        {
            if(pageNo < 0 || pageSize < 0) return BadRequest("Invalid page number or page size.");
            var productCategories = dbContext.TblProductCategories
                .Select(x => new
                {
                    x.ProductCategoryId,
                    x.ProductCategoryName
                }).ToList();
            return Ok(productCategories);
        }


    }
}
