using LTADotNetTrainingBatch3.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LTADotNetTrainingBatch3.WebAPI.Controllers
{
    public class ProductADOController : ControllerBase
    {
        private readonly IProductADOService _productADOService;
        public ProductADOController(IProductADOService productADOService)
        {
            _productADOService = productADOService;
        }
    }
}
