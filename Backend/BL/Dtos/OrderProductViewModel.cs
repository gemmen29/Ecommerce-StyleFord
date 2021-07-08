using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class OrderProductViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Total Price")]
        public double productTotal { get; set; }
        [Display(Name = "Discount")]
        public double productDiscount { get; set; }
        [Display(Name = "Net Price")]
        public double ProductNetPrice { get; set; }
        [Display(Name = "Quantity")]
        public int productQuantity { get; set; }
        public int ProductID { get; set; }
        public int orderID { get; set; }
       //public  Product prodct { get; set; }
       //public ProductDto productDto { get; set; }
       public string productName { get; set; }
    }
   
}
