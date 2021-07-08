using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
   public class RegisterViewodel
    {

        public string Id { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }

        //[DataType(DataType.Password)]
        //[Required]
        //[Compare("PasswordHash")]
        //[Display(Name = "Confirm Password")]
        //public string ConfirmPassword { get; set; }

     

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [MinLength(3)]
        public string  Country { get; set; }
        [DataType(DataType.Date)]

        public DateTime BirthDate { get; set; }

        [Required]
        [MinLength(3)]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string RoleName { get; set; }

        [Required]
        public bool isDeleted { get; set; }

        public string FullName { get; set; }
    }
}
