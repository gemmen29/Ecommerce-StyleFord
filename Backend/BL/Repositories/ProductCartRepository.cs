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
   public class ProductCartRepository : BaseRepository<ProductCart>
    {
        private DbContext EC_DbContext;

        public ProductCartRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public List<ProductCart> GetAllProductCart(string cartId)
        {
            return DbSet.Where(pc => pc.CartID == cartId).Include(pc => pc.product).ToList();
        }

        public bool InsertProductCart(ProductCart productCart)
        {
            return Insert(productCart);
        }

        public void DeleteProductCart(int id)
        {
            Delete(id);
        }
        //public void UpdateCategory(Category category)
        //{
        //    Update(category);
        //}


        //public bool CheckCategoryExists(Category category)
        //{
        //    return GetAny(l => l.ID == category.ID);
        //}
        //public Category GetOCategoryById(int id)
        //{
        //    return GetFirstOrDefault(l => l.ID == id);
        //}
        #endregion
    }
}
