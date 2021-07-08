using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
   public class OrderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        public string date { get; set; }

   

        [Display(Name = "Total Price")]
        public double totalPrice { get; set; }
        public string ApplicationUserIdentity_Id { get; set; }
        public virtual ApplicationUserIdentity appUser { get; set; }
    }
}
