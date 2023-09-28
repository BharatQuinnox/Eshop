using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.DataModel
{
    public class CartDataItemModel
    {

        public int? CartId { get; set; }

        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool IsLoyaltyMemberShip { get; set; }

        public const string InsertQuery = "INSERT INTO cart (CustomerId, ProductId, Quantity,CreatedDate, IsLoyaltyMemberShip)VALUES (@CustomerId, @ProductId, @Quantity,@CreatedDate, @IsLoyaltyMemberShip)";

        
        public const string SelectQuery = "SELECT CartId, CustomerId, ProductId, Quantity,CreatedDate, IsLoyaltyMemberShip FROM Cart";

        public const string DeleteQuery = "DELETE FROM Cart WHERE CustomerId = @CustomerId";

    }


}
