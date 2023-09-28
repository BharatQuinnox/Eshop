using AutoMapper;
using Dapper;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EShop.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly IDbConnectionFactory _dbConnectionFactory;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CustomerRepository(IDbConnectionFactory dbConnectionFactory,
                                  IConfiguration configuration, IMapper mapper)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<CustomerEntity> GetCustomerById(int CustomerId)
        {
            CustomerEntity customer = new CustomerEntity();

            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@CustomerId", CustomerId);

                dbConnection.Open();
                var parameters = new { Id = CustomerId };
                var customerdatamodel = await dbConnection.QueryFirstOrDefaultAsync<CustomerDataModel>(CustomerDataModel.SelectQuery, dynamicParameters, commandType: CommandType.Text);
                customer = _mapper.Map<CustomerEntity>(customerdatamodel);
                return customer;
            }
        }

        public async Task<bool> ActiveCustomerMemberShip(int CustomerId)
        {
            List<CartItemEntity> cartItemList = new List<CartItemEntity>();

            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@CustomerId", CustomerId);

                dbConnection.Open();

                var cartitem = dbConnection.Execute(CustomerDataModel.UpdateQuery, dynamicParameters, commandType: CommandType.Text);

                return true;
            }

        }
    }
}
