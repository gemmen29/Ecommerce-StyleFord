using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OrderProduct
    {
        public int ID { get; set; }

        public double productTotal { get; set; }
        public double productDiscount { get; set; }
        public double ProductNetPrice { get; set; }
        public int productQuantity { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
     
        public Product Product { get; set; }

        [ForeignKey("Order")]
        public int orderID { get; set; }
       
        public Order Order { get; set; }

    }
}
