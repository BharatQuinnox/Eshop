using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.DataModel
{
    public class ShippingDetailDataModel
    {
        public int CustomerId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }
        public bool IsActive { get; set; }

        public const string Insert = "INSERT INTO ShippingDetail(CustomerId, AddressLine1, AddressLine2, City, Country, ZipCode, IsActive) VALUES (@CustomerId, @AddressLine1, @AddressLine2, @City, @Country, @ZipCode, @IsActive)" +
                                     "SELECT CAST(SCOPE_IDENTITY() AS INT)";

        public const string Select = "SELECT  ShippingId, CustomerId, AddressLine1, AddressLine2, City, Country, ZipCode, IsActive  FROM ShippingDetail WHERE CustomerId = @CustomerId";
    }
}
