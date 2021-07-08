using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class ShoppingCartInfoViewModel
    {
        public double totalOrderPrice { get; set; }
        public List<int> quantites { get; set; }
    }
}
