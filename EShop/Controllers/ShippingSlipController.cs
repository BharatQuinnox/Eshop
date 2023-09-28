using EShop.Application.Dto;
using EShop.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingSlipController : ControllerBase
    {
        private readonly IOrderService _orderService;


        public ShippingSlipController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Generate Shipping slip
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GenerateSlipIfRequired")]
        public async Task<ActionResult> Post([FromBody] Order order)
        {
            bool IsGenerated = await _orderService.GenerateSlipIfRequired(order);
            if (IsGenerated)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
