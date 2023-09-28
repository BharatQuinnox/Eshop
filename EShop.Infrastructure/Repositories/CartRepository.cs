using AutoMapper;
using Dapper;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using EShop.Infrastructure.Consts;
using EShop.Infrastructure.DataModel;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EShop.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        protected readonly IDbConnectionFactory _dbConnectionFactory;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CartRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration, IMapper mapper)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<bool> AddCartItem(CartEntity purchaseRequest)
        {
            var cartModel = _mapper.Map<List<CartDataItemModel>>(purchaseRequest.Items);


            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    // Execute the insert query for each row in the DataTable
                    foreach (var row in cartModel)
                    {
                        dbConnection.Execute(CartDataItemModel.InsertQuery, row, transaction);
                    }

                    // Commit the transaction to save the changes to the database
                    transaction.Commit();
                }


            }

            return true;
        }
        public async Task<List<CartItemEntity>> getCartItem(int CustomerID)
        {
            List<CartItemEntity> cartItemList = new List<CartItemEntity>();

            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@CustomerId", CustomerID);

                dbConnection.Open();

                // Define the stored procedure call using Dapper
                var parameters = new { Id = CustomerID };
                var cartitem = await dbConnection.QueryAsync<CartDataItemModel>(CartDataItemModel.SelectQuery, dynamicParameters, commandType: CommandType.Text);

                cartItemList = _mapper.Map<List<CartItemEntity>>(cartitem);

                return cartItemList;
            }

        }
        public async Task<bool> Delete(int CustomerId)
        {
            List<CartItemEntity> cartItemList = new List<CartItemEntity>();

            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@CustomerId", CustomerId);

                dbConnection.Open();

                var cartitem =  dbConnection.Execute(CartDataItemModel.DeleteQuery, dynamicParameters, commandType: CommandType.Text);
 
                return true;
            }

        }


    }
}
