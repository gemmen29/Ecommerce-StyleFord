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
    public class ProductWishListRepository : BaseRepository<ProductWishList>
    {
      
        private DbContext EC_DbContext;

        public ProductWishListRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public List<ProductWishList> GetAllProductWishList()
        {
            return GetAll().ToList();
        }

        public bool InsertProductWishList(ProductWishList productWishList)
        {
            return Insert(productWishList);
        }
        public void DeleteProductWishList(int id)
        {
            Delete(id);
        }
        //public void UpdateCategory(Category category)
        //{
        //    Update(category);
        //}
        //public void DeleteCategory(int id)
        //{
        //    Delete(id);
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
