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
    public class CartRepository : BaseRepository<Cart>
    {
        private DbContext EC_DbContext;

        public CartRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }

        #region CRUB

        public List<Cart> GetAllCart()
        {
            return GetAll().ToList();
        }

        public bool InsertCart(Cart cart)
        {
            return Insert(cart);
        }
        public void UpdateCart(Cart cart)
        {
            Update(cart);
        }
        public void DeleteCart(int id)
        {
            Delete(id);
        }

        public bool CheckCartExists(Cart cart)
        {
            return GetAny(l => l.ID == cart.ID);
        }
        public Cart GetOCartById(string id)
        {
            return GetFirstOrDefault(l => l.ID == id);
        }
        #endregion
    }
}
