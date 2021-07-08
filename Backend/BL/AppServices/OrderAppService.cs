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
using AutoMapper;

namespace BL.AppServices
{
    public class OrderAppService : AppServiceBase
    {
        public OrderAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        #region CURD

        public List<OrderViewModel> GetAllOrder()
        {

            return Mapper.Map<List<OrderViewModel>>(TheUnitOfWork.Order.GetAllOrder());
        }
        public OrderViewModel GetOrder(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            return Mapper.Map<OrderViewModel>(TheUnitOfWork.Order.GetOrderById(id));
        }



        public bool SaveNewOrder(OrderViewModel orderViewModel)
        {
            if (orderViewModel == null)
                throw new ArgumentNullException();
            if (orderViewModel.ApplicationUserIdentity_Id == null || orderViewModel.ApplicationUserIdentity_Id == string.Empty)
                throw new ArgumentException();
            bool result = false;
            var order = Mapper.Map<Order>(orderViewModel);
            if (TheUnitOfWork.Order.Insert(order))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateOrder(OrderViewModel orderViewModel)
        {
            if (orderViewModel == null)
                throw new ArgumentNullException();
            if (orderViewModel.ApplicationUserIdentity_Id == null || orderViewModel.ApplicationUserIdentity_Id == string.Empty)
                throw new ArgumentException();
            var order = Mapper.Map<Order>(orderViewModel);
            TheUnitOfWork.Order.Update(order);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteOrder(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            bool result = false;

            TheUnitOfWork.Order.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckOrderExists(OrderViewModel orderViewModel)
        {
            if (orderViewModel == null)
                throw new ArgumentNullException();
            if (orderViewModel.ApplicationUserIdentity_Id == null || orderViewModel.ApplicationUserIdentity_Id == string.Empty)
                throw new ArgumentException();
            Order order = Mapper.Map<Order>(orderViewModel);
            return TheUnitOfWork.Order.CheckOrderExists(order);
        }
        #endregion
        public int CountEntity()
        {
            return TheUnitOfWork.Order.CountEntity();
        }
        public int CountEntityForSpecficUser(string userID)
        {
            return TheUnitOfWork.Order.CountEntityForSpeCifcUser(userID);
        }
        public IEnumerable<OrderViewModel> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<List<OrderViewModel>>(TheUnitOfWork.Order.GetPageRecords(pageSize, pageNumber));
        }
        public IEnumerable<OrderViewModel> GetPageRecordsForSpeceficUser(string userID,int pageSize, int pageNumber)
        {
            return Mapper.Map<List<OrderViewModel>>(TheUnitOfWork.Order.GetPageRecordsForSpeceficUser(userID,pageSize, pageNumber));
        }

    }
}
