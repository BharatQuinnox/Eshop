using AutoMapper;
using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Application.IServices;
using EShop.Application.Order;
using EShop.Application.Services;
using EShop.Application.Validation;
using EShop.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OnlineLibraryShop.Application.CustomServices
{
    public class OrderService : IOrderService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly IGenerateSlip _generateSlip;

        public OrderService(IMediator mediator, IGenerateSlip generateSlip, IMapper mapper)
        {
            _mediator = mediator;
            _generateSlip = generateSlip;
            _mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreatePurchaseOrder(Order order)
        {
            OrderEntity orderEntity = new OrderEntity();
            orderEntity = _mapper.Map<OrderEntity>(order);
            var products = await _mediator.Send(new GetProductByQuery());
            var cartItems = await _mediator.Send(new GetCartItemQuery() { CustomerId = order.CustomerId });
            var customer = await _mediator.Send(new GetCustomerByIdQuery() { CustomerId = order.CustomerId });
            decimal discountPercentage = 0.20M;
            int membershipFees = 5;


            if (products != null && products.Count > 0 && cartItems != null && cartItems.Count > 0 && customer != null)
            {
                var query = (from p in products
                             join
                             ci in cartItems on p.ProductId equals ci.ProductId
                             where ci.IsLoyaltyMemberShip == false
                             select new OrderEntity
                             {
                                 TransactionDate = ci.CreatedDate,
                                 OrderNumber = "PO-" + ci.CustomerId.ToString() + "-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                 CustomerId = ci.CustomerId,
                                 IsActive = true,
                                 ShipId = order.ShipId,
                                 Quantity = ci.Quantity,
                                 Price = p.Price
                             }).ToList();

                if (order.IncludeLoyaltyMemberShip == true || (customer.IsLoyaltyMembership && customer.MemberShipEndDate > DateTime.Now) || cartItems.Any(x=>x.IsLoyaltyMemberShip))
                {
                    decimal totalSum = query.Sum(result => result.Quantity * result.Price);
                    totalSum = totalSum * 0.80M;
                    totalSum = totalSum + membershipFees;
                    orderEntity = query[0];
                    if (orderEntity != null)
                    {
                        orderEntity.TotalAmount = totalSum;
                    }
                }
                else if (order.IncludeLoyaltyMemberShip == false && customer.IsLoyaltyMembership == false && cartItems.Any(x => x.IsLoyaltyMemberShip)==false)
                {
                    decimal totalSum = query.Sum(result => result.Quantity * result.Price);
                    orderEntity = query[0];
                    if (orderEntity != null)
                    {
                        orderEntity.TotalAmount = totalSum;
                    }
                }
            }

            CreateOrderCommand createOrderCommand = new CreateOrderCommand() { payload = orderEntity };

            var orderid = await _mediator.Send(createOrderCommand);
            if (orderid > 0)
            {

                List<OrderDetailEntity> orderDetailEntities = new List<OrderDetailEntity>();

                if (order.IncludeLoyaltyMemberShip == true || (customer.IsLoyaltyMembership && customer.MemberShipEndDate > DateTime.UtcNow) || cartItems.Any(x => x.IsLoyaltyMemberShip))
                {
                    orderDetailEntities = (from p in products
                                           join
                                           ci in cartItems on p.ProductId equals ci.ProductId
                                           where ci.IsLoyaltyMemberShip == false
                                           select new OrderDetailEntity
                                           {
                                               TransactionDate = DateTime.UtcNow,
                                               IsActive = true,
                                               OrderId = orderid,
                                               ProductId = p.ProductId,
                                               NetAmount =  (ci.Quantity * (p.Price - (p.Price * discountPercentage))),
                                               IsMembership = ci.IsLoyaltyMemberShip,
                                               Quantity = ci.Quantity,
                                               DiscountAmount =  (ci.Quantity * p.Price) - (ci.Quantity * (p.Price - (p.Price * discountPercentage))) 
                                           }).ToList();

                    if(cartItems.Any(x => x.IsLoyaltyMemberShip))
                    {
                      var membership =  cartItems.FirstOrDefault(x => x.IsLoyaltyMemberShip = true);
                        orderDetailEntities.Add(new OrderDetailEntity
                        {
                            DiscountAmount = 0,
                            IsActive = true,
                            OrderId = orderid,
                            IsMembership = membership.IsLoyaltyMemberShip,
                            NetAmount = membershipFees,
                            ProductId = membership.ProductId.Value,
                            Quantity = membership.Quantity,
                            TransactionDate = DateTime.UtcNow
                        });
                    }

                }
                else
                {
                    orderDetailEntities = (from p in products
                                           join
                                           ci in cartItems on p.ProductId equals ci.ProductId
                                           where ci.IsLoyaltyMemberShip == false
                                           select new OrderDetailEntity
                                           {
                                               TransactionDate = DateTime.UtcNow,
                                               IsActive = true,
                                               OrderId = orderid,
                                               ProductId = p.ProductId,
                                               NetAmount = 0,
                                               IsMembership = ci.IsLoyaltyMemberShip,
                                               Quantity = ci.Quantity,
                                               DiscountAmount = (ci.Quantity * p.Price)
                                           }).ToList();
                }
                CreateOrderDetailCommand createOrderDetailCommand = new CreateOrderDetailCommand() { payload= orderDetailEntities};

               var ordedetailResult =  await _mediator.Send(createOrderDetailCommand);


                

                if (ordedetailResult)
                {
                    CustomerEntity customerEntity = new CustomerEntity();
                    customerEntity.CustomerId = order.CustomerId;
                    UpdateCustomerMembershipCommand updateMembershipCommand=new UpdateCustomerMembershipCommand();
                    updateMembershipCommand.payload = customerEntity;
                    var updateMembershipResult = await _mediator.Send(updateMembershipCommand);
                    DeleteCartCommand deleteCartCommand = new DeleteCartCommand();
                    deleteCartCommand.payload = new CartItemEntity { CustomerId = order.CustomerId };
                    var deleteResulet = await _mediator.Send(deleteCartCommand);
                }



                return new ApiResponse<int>
                {
                    Data = orderid,
                    HasError = false,
                    Error = string.Empty,
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            else
            {
                return new ApiResponse<int>
                {
                    Data = orderid,
                    HasError = false,
                    Error = string.Empty,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<bool> GenerateSlipIfRequired(Order order)
        {
            ShippingSlipEntity shipping = new ShippingSlipEntity();

            var orderResponse = await _mediator.Send(new GetOrderByIdQuery { OrderId = order.OrderId });
            var shippingResponse = await _mediator.Send(new GetShippingAddressByIdQuery { CustomerId = order.CustomerId });

            shipping.orderDetails = orderResponse;
            shipping.shipping = shippingResponse;
            if (shipping.orderDetails.Count > 0)
            {
                return _generateSlip.GeneratePdf(shipping);
            }
            return false;

        }

        public async Task<ApiResponse<List<string>>> ValidateOrderData(Order command)
        {
            List<string> errorList = new List<string>();
            var customer = await _mediator.Send(new GetCustomerByIdQuery { CustomerId = command.CustomerId });

            if (customer == null)
            {
                errorList.Add("CustomerId is not valid");
            }

            var cartItem = await _mediator.Send(new GetCartItemQuery { CustomerId = command.CustomerId });
            if (cartItem.Count == 0)
            {
                errorList.Add("Cart should not be empty while place order");

            }
            if (errorList.Count > 0)
            {
                return new ApiResponse<List<string>>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Error = "invalid data",
                    HasError = true,
                    Data = errorList
                };
            }
            else
            {
                return new ApiResponse<List<string>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Error = string.Empty,
                    HasError = false,
                    Data = errorList
                };
            }
        }


        
    }
}
