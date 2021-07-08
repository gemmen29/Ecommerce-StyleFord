using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class CartAndPaymentInfoViewModel
    {
        public List<PaymentViewModel> paymentViewModels { get; set; }
        public PaymentViewModel paymentViewModel { get; set; } = new PaymentViewModel();
        public List<ProductViewModel> productViewModels { get; set; }
    }
}
