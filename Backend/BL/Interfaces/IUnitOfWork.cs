using BL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        #region Methode
        int Commit();
        #endregion

        OrderRepository Order { get; }
        CategoryRepository Category { get; }
        PaymentRepository Payment { get; }
        CartRepository Cart { get; }
        WishlistRepository Wishlist { get; }
        AccountRepository Account { get; }
        RoleRepository Role { get; }
        ProductRepository Product { get; }
        ColorRepository Color { get; }

        ProductCartRepository ProductCart { get; }
        ProductWishListRepository ProductWishList { get; }
        OrderProductRepository OrderProduct { get; }
        ReviewsRepository Review{ get; }

    }
}
