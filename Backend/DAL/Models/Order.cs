using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        public string date { get; set; }
      
        public double totalPrice { get; set; }

        [ForeignKey("appUser")]
        public string ApplicationUserIdentity_Id { get; set; }
        public ApplicationUserIdentity appUser { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
