using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.DataModel
{
    public class CustomerDataModel
    {
        public int CustomerId { get; set; }
        public string CustomerNumber { get; set; }
        public string Name { get; set; }
        public bool IsLoyaltyMembership { get; set; }
        public bool IsActive { get; set; }
        public DateTime MemberShipStartDate { get; set; }
        public DateTime MemberShipEndDate { get; set; }

        public const string SelectQuery = "SELECT CustomerId, CustomerNumber, Name, IsLoyaltyMembership, IsActive, MemberShipStartDate, MemberShipEndDate " +
                                "FROM Customer " +
                                "WHERE CustomerId = @CustomerId";

        public const string UpdateQuery = "UPDATE Customer " +
                " SET IsLoyaltyMembership = 1, " +
                " MemberShipStartDate = GETDATE(), " +
                " MemberShipEndDate = DATEADD(day, 30, GETDATE()) " +
                " WHERE CustomerId = @CustomerId";
    }

}

