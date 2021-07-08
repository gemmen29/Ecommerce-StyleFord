using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class ProductCartViewModel
    {
        public int ID { get; set; }
        public int productId { get; set; }
        public string cartId { get; set; }

        public string ProductImage { get; set; }
        public double ProductPrice { get; set; }
        public string ProductName { get; set; }
        public int ProductDiscount { get; set; }
        public int ProductQuantity { get; set; }

    }
}
