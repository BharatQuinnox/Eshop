using AutoMapper;
using EShop.Application.Command;
using EShop.Application.Command.Handler;
using EShop.Application.Dto;
using EShop.Application.IServices;
using EShop.Core.Entities;
using EShop.Infrastructure.Consts;
using MediatR;
using System.Linq;
using System.Net;

namespace EShop.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> AddCartItems(Cart cart)
        {

            CartEntity cartEntity = new CartEntity();

            cartEntity = _mapper.Map<CartEntity>(cart);

            foreach (var item in cartEntity.Items)
            {
                if (item.Type == (int)MembershipType.Membership)
                {
                    item.IsLoyaltyMemberShip = true;
                    item.CustomerId = cart.CustomerId;
                    item.CreatedDate = DateTime.Now;
                }
                else
                {
                    item.IsLoyaltyMemberShip = false;
                    item.CustomerId = cart.CustomerId;
                    item.CreatedDate = DateTime.Now;
                }
            }

            CreateCartCommand command = new CreateCartCommand() { payload = cartEntity };
            var result = await _mediator.Send(command);

            if (result == true)
            {
                return new ApiResponse<bool>()
                {
                    Data = true,
                    HasError = false,
                    Error = "",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            else
            {
                return new ApiResponse<bool>()
                {
                    Data = false,
                    HasError = true,
                    Error = "Error while create cart item",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<ApiResponse<List<CartItem>>> GetCartItem(int CustomerId)
        {
            List<CartItem> cartItems = new List<CartItem>();
            var result = await _mediator.Send(new GetCartItemQuery { CustomerId = CustomerId });
            cartItems = _mapper.Map<List<CartItem>>(result);

            if (cartItems.Count > 0)
            {
                return new ApiResponse<List<CartItem>>()
                {
                    Data = cartItems,
                    HasError = false,
                    Error = string.Empty,
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            else
            {
                return new ApiResponse<List<CartItem>>()
                {
                    Data = cartItems,
                    HasError = true,
                    Error = "No Data found",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
        }
        public async Task<ApiResponse<List<string>>> ValidatCartInvalidData(Cart cart)
        {
            List<string> customerErrors = new List<string>();
            var customer = await _mediator.Send(new GetCustomerByIdQuery { CustomerId = cart.CustomerId });

            if (customer == null)
            {
                customerErrors.Add("Customer is invalid");
            }
            
            if (customerErrors.Count() > 0)
            {
                return new ApiResponse<List<string>>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Data = customerErrors,
                    Error = "Invalid data",
                    HasError = true
                };
            }
            else
            {
                return new ApiResponse<List<string>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = customerErrors,
                    Error = string.Empty,
                    HasError = false
                };
            }
        }
    }

}
