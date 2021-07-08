using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Product")]
    public class Product
    {
       
        public int ID { get; set; }
        [Required]
        [MinLength(5)]
        //[RegularExpression("[a-zA-Z]{5,}", ErrorMessage = "Name must be only characters and more that 5")]
        public string Name { get; set; }

     
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid price")]
        public double Price { get; set; } //make it double instead of nullable

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [Range(5, int.MaxValue, ErrorMessage = "Discout Must be more than 5")]
        public double Discount{ get; set; }

        public string Image { get; set; }

      
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be more than 1")]
        public int Quantity { get; set; }

        [NotMapped]
        public double? AverageRating 
        {
            get { 
                if(Reviews.Count != 0)
                    return Reviews.Select(r => r.Rating).Average();
                return null;
            } 
        }


        [ForeignKey("Category")]
        public  int CategoryId { get; set; }
        public Category Category { get; set; }
    
        [ForeignKey("Color")]
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public List<ProductCart> Carts { get; set; } = new List<ProductCart>();
        public List<ProductWishList> Wishlists { get; set; } = new List<ProductWishList>();

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public List<Review> Reviews { get; set; } = new List<Review>();


    }
}
