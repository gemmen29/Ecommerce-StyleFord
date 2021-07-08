using BL.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Dtos;
using DAL.Models;
using BL.Interfaces;
using AutoMapper;

namespace BL.AppServices
{
     public class ProductCartAppService: AppServiceBase
    {
        public ProductCartAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public List<ProductCartViewModel> GetAllProductCart(string cartId)
        {

            return Mapper.Map<List<ProductCartViewModel>>(TheUnitOfWork.ProductCart.GetAllProductCart(cartId));
        }
   
        public bool SaveNewProductCart(ProductCart productCart)
        {
            if (productCart== null)
                throw new ArgumentNullException();
            bool result = false;
            if (TheUnitOfWork.ProductCart.Insert(productCart))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool DeleteProductCart(int id)
        {
            if(id<=0)
                throw new InvalidOperationException();
            bool result = false;

            TheUnitOfWork.ProductCart.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckIfProductExistsInCart(string cartID , int productID)
        {
            var isExistProductInCart = TheUnitOfWork.ProductCart
                .GetFirstOrDefault(c => c.CartID == cartID && c.productId == productID);
            return isExistProductInCart == null ? false : true;
        }

        public int GetProductCartID(string cartID, int productID)
        {
            return TheUnitOfWork.ProductCart
                .GetFirstOrDefault(c => c.CartID == cartID && c.productId == productID).ID;
        }

    }
}
