using BL.Bases;
using BL.Interfaces;
using BL.Dtos;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BL.AppServices
{
    public class OrderProductAppService : AppServiceBase
    {
        public OrderProductAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        #region CURD

        public List<OrderProductViewModel> GetAllOrderProduct()
        {

            return Mapper.Map<List<OrderProductViewModel>>(TheUnitOfWork.OrderProduct.GetAllOrderProduct());
        }
        //public CartViewModel GetCart(int id)
        //{
        //    return Mapper.Map<CartViewModel>(TheUnitOfWork.Cart.GetById(id));
        //}



        public bool SaveNewOrderProduct(OrderProductViewModel orderProductViewModel)
        {

            bool result = false;
            var orderProduct = Mapper.Map<OrderProduct>(orderProductViewModel);
            if (TheUnitOfWork.OrderProduct.Insert(orderProduct))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        /*public bool UpdateCategory(OrderViewModel orderViewModel)
        {
            var category = Mapper.Map<Category>(orderViewModel);
            TheUnitOfWork.Category.Update(category);
            TheUnitOfWork.Commit();

            return true;
        }*/


        //public bool DeleteCart(int id)
        //{
        //    bool result = false;

        //    TheUnitOfWork.Cart.Delete(id);
        //    result = TheUnitOfWork.Commit() > new int();

        //    return result;
        //}

        /*public bool CheckCategoryExists(OrderViewModel orderViewModel)
        {
            Category category = Mapper.Map<Category>(orderViewModel);
            return TheUnitOfWork.Category.CheckCategoryExists(category);
        }*/
        #endregion
    }
}
