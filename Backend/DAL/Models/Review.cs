using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Review")]
    public class Review
    {
        public int ID { get; set; }
        public string Comment { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public ApplicationUserIdentity User { get; set; }
    }
}
