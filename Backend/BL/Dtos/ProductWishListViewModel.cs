using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
   public class ProductWishListViewModel
    {
        public int ID { get; set; }
        public int productId { get; set; }
        public string wishlistId { get; set; }
    }
}
