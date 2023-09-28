using AutoMapper;
using EShop.Application.Command;
using EShop.Application.Command.Handler;
using EShop.Application.Dto;
using EShop.Application.IServices;
using EShop.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        public async Task<ApiResponse<List<Product>>> GetProductList()
        {
            List<Product> products = new List<Product>();
            var result = await _mediator.Send(new GetProductByQuery());
            products = _mapper.Map<List<Product>>(result);
            return new ApiResponse<List<Product>>()
            {
                Data = products,
                HasError = false,
                Error = string.Empty,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
