using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Application.IServices;
using EShop.Application.Order;
using EShop.Application.Services;
using EShop.Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OnlineLibraryShop.Application.CustomServices;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IValidator<Order> _validator;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService orderService, ICartService cartService, IValidator<Order> validator, ICustomerService customerService)
        {
            _orderService = orderService;
            _cartService = cartService;
            _validator = validator;
            _customerService = customerService;
        }
        /// <summary>
        /// User can place Order
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult> Post([FromBody]  Order order)
        {
            var validationResult = _validator.Validate(order);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var dataValidationResult = await _orderService.ValidateOrderData(order);
            if (dataValidationResult.HasError)
            {
                return BadRequest(dataValidationResult.Data);
            }
            var result = await _orderService.CreatePurchaseOrder(order);
            if (result.HasError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(Post), new { id = result.Data }, new { GenerateOrderId = result.Data });
            }
        }
        
     }
}
