using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductCart
    {
        public int ID { get; set; }

        [ForeignKey("product")]
        public int productId{ get; set; }
        public Product product { get; set; }


        [ForeignKey("cart")]
        public string CartID { get; set; }
        public Cart cart { get; set; }
    }
}
