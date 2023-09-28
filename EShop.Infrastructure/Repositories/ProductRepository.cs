using AutoMapper;
using Dapper;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        protected readonly IDbConnectionFactory _dbConnectionFactory;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ProductRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration, IMapper mapper)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<List<ProductEntity>> GetProductList()
        {
            List<ProductEntity> productList = new List<ProductEntity>();

            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                dbConnection.Open();
                IEnumerable<ProductDataModel> products = await dbConnection.QueryAsync<ProductDataModel>(ProductDataModel.SelectQuery, commandType: CommandType.Text);
                var result = products.ToList();
                productList = _mapper.Map<List<ProductEntity>>(result);
                return productList;
            }
        }
    }
}
