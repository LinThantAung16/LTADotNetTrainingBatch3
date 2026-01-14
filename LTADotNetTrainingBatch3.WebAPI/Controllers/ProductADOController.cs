using LTADotNetTrainingBatch3.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LTADotNetTrainingBatch3.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductADOController : ControllerBase
    {
        private readonly IProductADOService _productADOService; //constructor injection
        // Constructor Injection
        // Method Injection
        // Property Injection
        //[Inject]
        //private IProductAdoDotNetService ProductAdoDotNetService { get; set; }
        public ProductADOController(IProductADOService productADOService)
        {
            _productADOService = productADOService;
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetProducts(int pageNo, int pageSize)
        {
            var response = _productADOService.GetProducts(pageNo, pageSize);
            if (!response.IsSuccess)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById (int id)
        {
            var response = _productADOService.GetProduct(id);
            if (!response.IsSuccess)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductCreateRequestDto request)
        {
            var response = _productADOService.CreateProduct(request);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchProduct(int id, ProductPatchRequestDto request)
        {
            var response = _productADOService.PatchProduct(id, request);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductUpdateRequestDto request)
        {
            var response = _productADOService.UpdateProduct(id, request);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var response = _productADOService.DeleteProduct(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
