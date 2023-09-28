using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using MediatR;
using System.Net;

namespace EShop.Application.Command.Handler
{
    public class GetProductQueryHandler : IRequestHandler<GetProductByQuery , List<ProductEntity>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductEntity>> Handle(GetProductByQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductList();
            return products;
        }

    }
}
