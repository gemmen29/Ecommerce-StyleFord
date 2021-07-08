using BL.AppServices;
using BL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderAppService _orderAppService;
        CartAppService _cartAppService;
        ProductCartAppService _productCartAppService ;
        ProductAppService _productAppService;
        OrderProductAppService _orderProductAppService;
        IHttpContextAccessor _httpContextAccessor;
        public OrderController(OrderAppService orderAppService,
            CartAppService cartAppService,
            ProductCartAppService productCartAppService,
            ProductAppService productAppService,
            OrderProductAppService orderProductAppService,
            IHttpContextAccessor httpContextAccessor)
        {
            this._orderAppService = orderAppService;
            this._cartAppService = cartAppService;
            this._productCartAppService = productCartAppService;
            this._productAppService = productAppService;
            this._orderProductAppService = orderProductAppService;
            this._httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok(_orderAppService.GetAllOrder());
        }


        [HttpPost]
        public IActionResult makeOrder(OrderDetailsViewModel orderDetailsViewModel)//, double totalOrderPrice)
        {
          
            //get cart id of current logged user
       
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            OrderViewModel orderViewModel = new OrderViewModel
            {
                date = DateTime.Now.ToString(),

                totalPrice = orderDetailsViewModel.totalOrderPrice,
                ApplicationUserIdentity_Id = userID

            };
            _orderAppService.SaveNewOrder(orderViewModel);
            var lastOrder = _orderAppService.GetAllOrder().Select(o => o.Id).LastOrDefault();

            //get know details of each product
            for (int i = 0; i < orderDetailsViewModel.productCartDetails.Count; i++)
            {
                var current = orderDetailsViewModel.productCartDetails[i];
                //var productViewModel = _productAppService.GetProduct(prodIds[i]);
                double totalOrder = current.productPrice * current.quantity;
                double AfterDiscount = totalOrder - totalOrder * (current.productDiscount / 100);
                OrderProductViewModel orderProductViewModel = new OrderProductViewModel
                {
                    orderID = lastOrder,
                    ProductID = current.productId,
                    productDiscount = current.productDiscount,
                    productQuantity = current.quantity,
                    productTotal = totalOrder,
                    ProductNetPrice = AfterDiscount
                };
                _orderProductAppService.SaveNewOrderProduct(orderProductViewModel);
                //decrease amount of product
                _productAppService.DecreaseQuantity(current.productId, current.quantity);

              var productCartID=  _productCartAppService.GetProductCartID(userID, current.productId);

       
                _productCartAppService.DeleteProductCart(productCartID);

            }

        
            return Ok();
        }

        

        //[HttpGet]
        //Route[("api/[controller]/{id}")]
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
           
            var orderProductViewModels = _orderProductAppService.GetAllOrderProduct().Where(op => op.orderID == id).ToList();
            //foreach (var item in orderProductViewModels)
            //{
            //    item.productName = _productAppService.GetProduct(item.ProductID).Name;
            //}

            return Ok(_orderProductAppService.GetAllOrderProduct().Where(op => op.orderID == id).ToList());
           //return Ok(orderProductViewModels);
        }
        [HttpGet("count")]
        public IActionResult OrderCount()
        {
            return Ok(_orderAppService.CountEntity());
        }
        [HttpGet("countOrdersForSpecifcUser/{userID}")]
        public IActionResult OrderCount(string userID)
        {
            return Ok(_orderAppService.CountEntityForSpecficUser(userID));
        }
        [HttpGet("{pageSize}/{pageNumber}")]
        public IActionResult GetOrdersByPage(int pageSize, int pageNumber)
        {
            var list = _orderAppService.GetPageRecords(pageSize, pageNumber);
            return Ok(_orderAppService.GetPageRecords(pageSize, pageNumber));
        }
        [HttpGet("{userID}/{pageSize}/{pageNumber}")]
        public IActionResult GetOrdersByPageForSpecficUser(string userID,int pageSize, int pageNumber)
        {
          
            return Ok(_orderAppService.GetPageRecordsForSpeceficUser(userID,pageSize, pageNumber));
        }
    }
}
