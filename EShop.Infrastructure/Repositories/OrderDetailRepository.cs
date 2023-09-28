using AutoMapper;
using Dapper;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        protected readonly IDbConnectionFactory _dbConnectionFactory;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public OrderDetailRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration, IMapper mapper)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<bool> AddOrderDetail(List<OrderDetailEntity> orderDetailEntity)
        {
            var orderdetailModal = _mapper.Map<List<OrderDetailDataModel>>(orderDetailEntity);


            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    // Execute the insert query for each row in the DataTable
                    foreach (var row in orderdetailModal)
                    {
                        dbConnection.Execute(OrderDetailDataModel.InsertQuery, row, transaction);
                    }

                    transaction.Commit();
                }
            }

            return  true;
        }
 

        
    }
}
