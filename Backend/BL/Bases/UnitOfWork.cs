using BL.Interfaces;
using BL.Repositories;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Common Properties
        private DbContext EC_DbContext { get; set; }
        private UserManager<ApplicationUserIdentity> _userManager;
        private RoleManager<IdentityRole> _roleManager;
  

        #endregion

        #region Constructors
        public UnitOfWork(ApplicationDBContext EC_DbContext, UserManager<ApplicationUserIdentity> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this.EC_DbContext = EC_DbContext;//
      

            // Avoid load navigation properties
            //EC_DbContext.Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        #region Methods
        public int Commit()
        {
            return EC_DbContext.SaveChanges();
        }

        public void Dispose()
        {
            EC_DbContext.Dispose();
        }
        #endregion


        public OrderRepository order;//=> throw new NotImplementedException();
        public OrderRepository Order
        {
            get
            {
                if (order == null)
                    order = new OrderRepository(EC_DbContext);
                return order;
            }
        }
        public ProductRepository product;//=> throw new NotImplementedException();
        public ProductRepository Product
        {
            get
            {
                if (product == null)
                    product = new ProductRepository(EC_DbContext);
                return product;
            }
        }
        public OrderProductRepository orderProduct;//=> throw new NotImplementedException();
        public OrderProductRepository OrderProduct
        {
            get
            {
                if (orderProduct == null)
                    orderProduct = new OrderProductRepository(EC_DbContext);
                return orderProduct;
            }
        }

        public CategoryRepository category;//=> throw new NotImplementedException();
        public CategoryRepository Category
        {
            get
            {
                if (category == null)
                    category = new CategoryRepository(EC_DbContext);
                return category;
            }
        }

        public ProductCartRepository productCart;//=> throw new NotImplementedException();
        public ProductCartRepository ProductCart
        {
            get
            {
                if (productCart == null)
                    productCart = new ProductCartRepository(EC_DbContext);
                return productCart;
            }
        }

        public ProductWishListRepository productWishList;//=> throw new NotImplementedException();
        public ProductWishListRepository ProductWishList
        {
            get
            {
                if (productWishList == null)
                    productWishList = new ProductWishListRepository(EC_DbContext);
                return productWishList;
            }
        }

        public PaymentRepository payment;//=> throw new NotImplementedException();
        public PaymentRepository Payment
        {
            get
            {
                if (payment == null)
                    payment = new PaymentRepository(EC_DbContext);
                return payment;
            }
        }

        public CartRepository cart;//=> throw new NotImplementedException();
        public CartRepository Cart
        {
            get
            {
                if (cart == null)
                    cart = new CartRepository(EC_DbContext);
                return cart;
            }
        }

        public WishlistRepository wishlist;//=> throw new NotImplementedException();
        public WishlistRepository Wishlist
        {
            get
            {
                if (wishlist == null)
                    wishlist = new WishlistRepository(EC_DbContext);
                return wishlist;
            }
        }

        public AccountRepository account;//=> throw new NotImplementedException();
        public AccountRepository Account
        {
            get
            {
                if (account == null)
                    account = new AccountRepository(EC_DbContext,_userManager,_roleManager);
                return account;
            }
        }

        public RoleRepository role;//=> throw new NotImplementedException();
        public RoleRepository Role
        {
            get
            {
                if (role == null)
                    role = new RoleRepository(EC_DbContext,_roleManager);
                return role;
            }
        }



        public ReviewsRepository review;//=> throw new NotImplementedException();
        public ReviewsRepository Review
        {
            get
            {
                if (review == null)
                    review = new ReviewsRepository(EC_DbContext);
                return review;
            }
        }

        public ColorRepository color;
        public ColorRepository Color
        {
            get
            {
                if (color == null)
                    color = new ColorRepository(EC_DbContext);
                return color;
            }
        }
    }
}
