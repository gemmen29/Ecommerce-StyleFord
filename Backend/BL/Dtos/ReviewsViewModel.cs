using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class ReviewsViewModel
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public int ProductID { get; set; }
        public string UserID { get; set; }
        public string UserFullName { get; set; }
    }
}
