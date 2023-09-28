using AutoMapper;
using Dapper;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EShop.Infrastructure.Repositories
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ShippingRepository(IDbConnectionFactory dbConnectionFactory,
                                              IConfiguration configuration,
                                              IMapper mapper)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<int> InsertShippingData(ShippingEntity shipping)
        {
            var shippingDataModel = _mapper.Map<ShippingDetailDataModel>(shipping);
            int id = 0;

            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // Open the connection
                dbConnection.Open();

                // Call the stored procedure using Dapper
                id =  dbConnection.Query<int>( ShippingDetailDataModel.Insert, shippingDataModel, commandType: CommandType.Text).Single();
            }
            return id;
        }

        public async Task<List<ShippingEntity>> GetShippingData(int CustomerId)
        {
            List<ShippingEntity> shippingList = new List<ShippingEntity>();

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@CustomerId", CustomerId);
            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                dbConnection.Open();
                IEnumerable<ShippingDetailDataModel> shippingdata = await dbConnection.QueryAsync<ShippingDetailDataModel>(ShippingDetailDataModel.Select, dynamicParameters, commandType: CommandType.Text);
                var result = shippingdata.ToList();
                shippingList = _mapper.Map<List<ShippingEntity>>(result);
                return shippingList;
            }
        }
    }
}
