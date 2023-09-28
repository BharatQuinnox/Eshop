using EShop.Application.Command;
using EShop.Application.IServices;
using log4net.Util;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    /// <summary>
    /// Product controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
       
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get List of Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult> Get()
        {
            var result  = await _productService.GetProductList();
            return Ok(result.Data);
        }

    }
}
