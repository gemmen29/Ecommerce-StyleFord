using AutoMapper;
using BL.Bases;
using BL.Interfaces;
using BL.Dtos;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL.AppServices
{
    public class PaymentAppService : AppServiceBase
    {
        public PaymentAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        #region CURD

        public List<PaymentViewModel> GetAllPayments()
        {

            return Mapper.Map<List<PaymentViewModel>>(TheUnitOfWork.Payment.GetAllPayment());
        }
        public List<PaymentViewModel> GetPaymentsOfUser(string id)
        {
            if (id == null || id=="")
                throw new ArgumentNullException();

            return GetAllPayments().Where(p => p.ApplicationUserIdentity_Id == id).ToList();
        }
        public PaymentViewModel GetPayment(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            return Mapper.Map<PaymentViewModel>(TheUnitOfWork.Payment.GetById(id));
           
        }
        public bool SaveNewPayment(PaymentViewModel paymentViewModel)
        {
            if (paymentViewModel == null)
                throw new ArgumentNullException();
            if (paymentViewModel.ApplicationUserIdentity_Id == null || paymentViewModel.ApplicationUserIdentity_Id == string.Empty)
                throw new ArgumentException();

            bool result = false;
            var payment = Mapper.Map<Payment>(paymentViewModel);
            if (TheUnitOfWork.Payment.Insert(payment))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdatePayment(PaymentViewModel paymentViewModel)
        {
            if (paymentViewModel == null)
                throw new ArgumentNullException();
            if (paymentViewModel.ApplicationUserIdentity_Id == null || paymentViewModel.ApplicationUserIdentity_Id == string.Empty)
                throw new ArgumentException();
            var payment = Mapper.Map<Payment>(paymentViewModel);
            TheUnitOfWork.Payment.Update(payment);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeletePayment(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            bool result = false;

            TheUnitOfWork.Payment.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckPaymentExists(PaymentViewModel paymentViewModel)
        {
            if (paymentViewModel == null)
                throw new ArgumentNullException();
            if (paymentViewModel.ApplicationUserIdentity_Id == null || paymentViewModel.ApplicationUserIdentity_Id == string.Empty)
                throw new ArgumentException();

            Payment payment = Mapper.Map<Payment>(paymentViewModel);
            return TheUnitOfWork.Payment.CheckPaymentExists(payment);
        }

  

       
        #endregion
    }
}
