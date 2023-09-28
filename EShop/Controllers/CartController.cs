using EShop.Application.Command;
using EShop.Application.IServices;
using EShop.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using EShop.Application.Services;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using EShop.Application.Dto;

namespace EShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private IValidator<Cart> _validator;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private ILogger<CartController> _logger;

        public CartController(ICartService cartService,
                              IValidator<Cart> validator,
                              IProductService productService,
                              ICustomerService customerService,
                              ILogger<CartController> logger)
        {
            _cartService = cartService;
            _validator = validator;
            _productService = productService;
            _customerService = customerService;
            _logger = logger;

        }
        /// <summary>
        /// Return Cart Details
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns>ii</returns>
        // GET: api/<CartController>
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult> Get(int CustomerId)
        {
            var result = await _cartService.GetCartItem(CustomerId);
            if (result.HasError)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Create Cart functionality
        /// </summary>
        /// <param name="cartRequestCommand"></param>
        /// <returns></returns>
        // POST api/<CartController>
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult> Post([FromBody] Cart cart)
        {
            _logger.LogInformation("Start Processing cart addding");

            var result = await _validator.ValidateAsync(cart);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            var validateResponse = await _cartService.ValidatCartInvalidData(cart);
            if (validateResponse.HasError)
            {
                return BadRequest(validateResponse.Data);
            }
            var createCardResponse = await _cartService.AddCartItems(cart);
            _logger.LogInformation("End Processing cart addding");
            if (createCardResponse.HasError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(Post), new { id = createCardResponse.Data }, new { GenerateOrderId = createCardResponse.Data });
            }

        }

    }
}
