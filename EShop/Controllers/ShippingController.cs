using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Application.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace EShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService _shippingService;
        private IValidator<Shipping> _validator;

        public ShippingController(IShippingService shippingService, IValidator<Shipping> validator)
        {
            _shippingService = shippingService;
            _validator = validator;
        }
        /// <summary>
        /// Get Shipping Detail For customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        // GET api/<ShippingController>/5
        [HttpGet()]
        [Produces("application/json")]
        public async Task<ActionResult> Get(int CustomerId)
        {
            var result = await _shippingService.GetShippingData(CustomerId);
            if(result.HasError)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Create Shipping Date for customer
        /// </summary>
        /// <param name="shipping"></param>
        /// <returns></returns>
        // POST api/<ShippingController>
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult> Post([FromBody] Shipping shipping)
        {
            var result = await _validator.ValidateAsync(shipping);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            var shippingResult = await _shippingService.InsertShippingAddress(shipping);
            if (shippingResult.HasError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(Post), new { id = shippingResult.Data }, new { ShippingId = shippingResult.Data });
            }
        }
    }
}
