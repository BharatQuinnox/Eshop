using EShop.Application.AutoMapper;
using EShop.Application.Command;
using EShop.Application.Command.Handler;
using EShop.Application.IServices;
using EShop.Application.Order;
using EShop.Application.Services;
using EShop.Application.Validation;
using EShop.Core.Interfaces;
using EShop.Infrastructure.ConnectionFactory;
using EShop.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using OnlineLibraryShop.Application.Command.Handler;
using OnlineLibraryShop.Application.CustomServices;
using OnlineLibraryShop.Core.Interfaces;

namespace EShop.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
       
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IGenerateSlip, GenerateSlip>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetOrderByIdQueryHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCartCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCartQueryHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCustomerByIdQueryHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderDetailCommandHandler).Assembly));
            services.AddValidatorsFromAssemblyContaining<CreateCartRequestCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateShippingCommandValidator>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddAutoMapper(typeof(ApplicationMapper));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDbConnectionFactory, SqlDBConnectionFactory>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IShippingRepository, ShippingRepository>();
            services.AddScoped<IShippingService, ShippingService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();




            return services;
        }
    }
}
