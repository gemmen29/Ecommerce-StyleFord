using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class OrderDetailsViewModel
    {
        public List<ProductQuantityForMakingOrderViewModel> productCartDetails { get; set; } = new List<ProductQuantityForMakingOrderViewModel>();
        public double totalOrderPrice { get; set; }
    }
}
