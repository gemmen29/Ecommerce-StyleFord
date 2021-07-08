using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.AppServices;
using BL.Dtos;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private PaymentAppService _paymentAppService;
        private IHttpContextAccessor _httpContextAccessor;
        public PaymentController(PaymentAppService paymentAppService, IHttpContextAccessor httpContextAccessor)
        {
            _paymentAppService = paymentAppService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult GetAllPayment()
        {
            var payments = _paymentAppService.GetAllPayments();
            return Ok(payments);
        }


        [HttpPost]
        public IActionResult Create(PaymentViewModel paymentViewModel)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            paymentViewModel.ApplicationUserIdentity_Id = userID;
            var payments = _paymentAppService.GetAllPayments();
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            _paymentAppService.SaveNewPayment(paymentViewModel);

            return Ok();
        }


    }
}
