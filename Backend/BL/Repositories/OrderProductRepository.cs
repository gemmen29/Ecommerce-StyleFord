using BL.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
   public class OrderProductRepository:BaseRepository<OrderProduct>
    {
        private DbContext EC_DbContext;

        public OrderProductRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }

      

        public List<OrderProduct> GetAllOrderProduct()
        {
            return GetAll().Include(op=>op.Product).ToList();
        }

        public bool InsertOrderProduct(OrderProduct orderProduct)
        {
            return Insert(orderProduct);
        }
        public void UpdateOrderProduct(OrderProduct orderProduct)
        {
            Update(orderProduct);
        }
        //public void DeleteCart(int id)
        //{
        //    Delete(id);
        //}

        //public bool CheckCartExists(Cart cart)
        //{
        //    return GetAny(l => l.ID == cart.ID);
        //}
        //public Cart GetOCartById(int id)
        //{
        //    return GetFirstOrDefault(l => l.ID == id);
        //}

    }
}
