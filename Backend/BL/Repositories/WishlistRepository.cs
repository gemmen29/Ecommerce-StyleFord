using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class WishlistRepository : BaseRepository<Wishlist>
    {
        private DbContext EC_DbContext;

        public WishlistRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }

        #region CRUB

        public List<Wishlist> GetAllWishlist()
        {
            return GetAll().ToList();
        }

        public bool InsertWishlist(Wishlist wishlist)
        {
            return Insert(wishlist);
        }
        public void UpdateWishlist(Wishlist wishlist)
        {
            Update(wishlist);
        }
        public void DeleteWishlist(int id)
        {
            Delete(id);
        }

        public bool CheckWishlistExists(Wishlist wishlist)
        {
            return GetAny(l => l.ID == wishlist.ID);
        }
        public Wishlist GetWishlistById(string id)
        {
            return GetFirstOrDefault(l => l.ID == id);
        }
        #endregion
    }
}
