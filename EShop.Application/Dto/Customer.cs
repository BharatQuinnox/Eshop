using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Dto
{
    public class Customer
    {

        public int CustomerId { get; set; }
        public string CustomerNumber { get; set; }
        public string Name { get; set; }
        public bool IsLoyaltyMembership { get; set; }
        public bool IsActive { get; set; }
        public DateTime MemberShipStartDate { get; set; }
        public DateTime MemberShipEndDate { get; set; }
    }
}
