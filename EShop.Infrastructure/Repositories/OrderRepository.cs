using AutoMapper;
using Dapper;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using Microsoft.Extensions.Configuration;
using OnlineLibraryShop.Core.Interfaces;
using System.Data;
using static Dapper.SqlMapper;

namespace EShop.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public OrderRepository(IDbConnectionFactory dbConnectionFactory,
                               IConfiguration configuration, IMapper mapper)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<int> CreateOrder(OrderEntity purchaseRequest)
        {

            var orderModel = _mapper.Map<OrderDataModel>(purchaseRequest);


            int orderid = 0;


            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // Open the connection
                dbConnection.Open();
                // Call the stored procedure using Dapper
                orderid = dbConnection.Query<int>(OrderDataModel.Insert, orderModel, commandType: CommandType.Text).Single();
            }

            return orderid;
        }

        public async Task<List<OrderEntity>> GetOrder(int PurchaseOrderId)
        {
            List<OrderEntity> purchaseOrderList = new List<OrderEntity>();
            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                dbConnection.Open();
                var parameters = new { Id = PurchaseOrderId };
                var result = await dbConnection.QueryAsync<ShippingSlipDataModel>(ShippingSlipDataModel.SelectQuery, parameters, commandType: CommandType.Text);
                purchaseOrderList = _mapper.Map<List<OrderEntity>>(result);
                return purchaseOrderList;
            }
        }
    }
}
