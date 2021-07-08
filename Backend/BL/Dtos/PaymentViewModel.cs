using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class PaymentViewModel
    {
        public int ID { get; set; }
        [Required]
        [MinLength(16), MaxLength(16)]
        [RegularExpression("[0-9]+",ErrorMessage ="Card must be numbers only ")]
        public string CardNumber { get; set; }


        [DataType(DataType.Date)]
        public DateTime ExperationDate { get; set; }
        public string cardOwnerName { get; set; }

        [MinLength(3), MaxLength(3)]
        [RegularExpression("[0-9]+", ErrorMessage = "CVV must be numbers only ")]
        public string cvc { get; set; }
        public string ApplicationUserIdentity_Id { get; set; }
    }
}
