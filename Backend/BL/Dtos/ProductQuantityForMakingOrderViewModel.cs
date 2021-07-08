using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
   public class ProductQuantityForMakingOrderViewModel
    {
        public int productId { get; set; }
        public int quantity { get; set; }
 
        public double productDiscount { get; set; }
        public double productPrice { get; set; }
    }
}
