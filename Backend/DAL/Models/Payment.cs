using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Payment")]
    public class Payment
    {
        public int ID { get; set; }
        [Required]
        [MinLength(16),MaxLength(16)]
        public string CardNumber { get; set; }


        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]

        public DateTime ExperationDate { get; set; }

        [Required]
        public string cardOwnerName { get; set; }

        [Required]
        [MinLength(3), MaxLength(3)]
        public string cvc { get; set; }

        [ForeignKey("ApplicationUserIdentity")]
        public string ApplicationUserIdentity_Id { get; set; }
        public ApplicationUserIdentity ApplicationUserIdentity { get; set; }
    }
}
