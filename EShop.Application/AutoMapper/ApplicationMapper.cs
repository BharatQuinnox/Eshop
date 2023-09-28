using AutoMapper;
using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Application.Order;
using EShop.Core.Entities;
using EShop.Infrastructure.DataModel;

namespace EShop.Application.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<PurchaseRequestDto, CreateOrderCommand>().ReverseMap();
            CreateMap<CreateCartCommand, CartPurchaseItems>().ReverseMap();
            CreateMap<Cart, CartEntity>().ReverseMap();
            CreateMap<Dto.Order, OrderEntity>().ReverseMap();
            CreateMap<Product, ProductEntity>().ReverseMap();
            CreateMap<Customer, CustomerEntity>().ReverseMap();
            CreateMap<CartItem, CartItemEntity>().ReverseMap();
            //CreateMap<CartEntity, CartDataItemModel>();
            CreateMap<ProductDataModel, ProductEntity>().ReverseMap();
            CreateMap<ShippingDetailDataModel, ShippingEntity>().ReverseMap();
            CreateMap<Dto.Shipping, ShippingEntity>().ReverseMap();
            CreateMap<OrderEntity, OrderDataModel>().ReverseMap();
            //CreateMap<CartEntity, CartDataModel>().ReverseMap();

            CreateMap<CartItemEntity, CartDataItemModel>()
                            .ReverseMap();

            CreateMap<OrderDetailEntity, OrderDetailDataModel>()
                            .ReverseMap();

            CreateMap<CustomerDataModel, CustomerEntity>()
                            .ReverseMap();

            CreateMap<ShippingSlipDataModel, OrderEntity>()
                            .ReverseMap();

        }
    }
}
    

