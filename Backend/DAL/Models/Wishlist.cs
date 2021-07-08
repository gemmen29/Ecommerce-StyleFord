using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Wishlist")]
    public class Wishlist
    {
        [ForeignKey("ApplicationUserIdentity")]
        public string ID { get; set; }
        public List<ProductWishList> Wishlists { get; set; } = new List<ProductWishList>();
        
        public ApplicationUserIdentity ApplicationUserIdentity { get; set; }
    }
}
