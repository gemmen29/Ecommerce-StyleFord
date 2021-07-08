using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    //this view model will be used by signal r to notify admin after each checkout 
    //by each product and the quantity that demanded by customer

    public class ProductQuantitiesCheckoutViewModel
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
