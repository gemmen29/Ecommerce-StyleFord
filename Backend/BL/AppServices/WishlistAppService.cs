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
    public class WishlistAppService : AppServiceBase
    {
        public WishlistAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        #region CURD

        public List<WishlistViewModel> GetAllWishlists()
        {
            return Mapper.Map<List<WishlistViewModel>>(TheUnitOfWork.Wishlist.GetAllWishlist());
        }
        public WishlistViewModel GetWishlist(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();
            return Mapper.Map<WishlistViewModel>(TheUnitOfWork.Wishlist.GetById(id));
        }



        //public bool SaveNewWishlist(WishlistViewModel wishlistViewModel)
        //{
        //    if (wishlistViewModel == null)
        //        throw new ArgumentNullException();

        //    bool result = false;
        //    var wishlist = Mapper.Map<Wishlist>(wishlistViewModel);
        //    if (TheUnitOfWork.Wishlist.Insert(wishlist))
        //    {
        //        result = TheUnitOfWork.Commit() > new int();
        //    }
        //    return result;
        //}
        public bool CreateUserWishlist(string userId)
        {
            bool result = false;
            Wishlist userWishlist = new Wishlist() { ID = userId };
            if (TheUnitOfWork.Wishlist.Insert(userWishlist))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool DeleteWishlist(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();
            bool result = false;

            TheUnitOfWork.Wishlist.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        #endregion
    }
}
