using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Cart")]
    public class Cart
    {
        [ForeignKey("ApplicationUserIdentity"), Key]
        public string ID { get; set; }
        public List<ProductCart> Products { get; set; } = new List<ProductCart>();
        public ApplicationUserIdentity ApplicationUserIdentity { get; set; }
    }
}
