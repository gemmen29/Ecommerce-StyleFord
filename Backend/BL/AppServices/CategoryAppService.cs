using BL.Bases;
using BL.Interfaces;
using BL.Dtos;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BL.AppServices
{
   public class CategoryAppService:AppServiceBase
    {
        public CategoryAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        #region CURD

        public List<CategoryViewModel> GetAllCateogries()
        {

            return Mapper.Map<List<CategoryViewModel>>(TheUnitOfWork.Category.GetAllCategory());
        }
        public CategoryViewModel GetCategory(int id)
        {
            return Mapper.Map<CategoryViewModel>(TheUnitOfWork.Category.GetById(id));
        }



        public bool SaveNewCategory(CategoryViewModel categoryViewModel)
        {
              if (categoryViewModel == null)
              
  throw new ArgumentNullException();

            bool result = false;
            var category = Mapper.Map<Category>(categoryViewModel);
            if (TheUnitOfWork.Category.Insert(category))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateCategory(CategoryViewModel categoryViewModel)
        {
            var category = Mapper.Map<Category>(categoryViewModel);
            TheUnitOfWork.Category.Update(category);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteCategory(int id)
        {
            bool result = false;

            TheUnitOfWork.Category.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckCategoryExists(CategoryViewModel categoryViewModel)
        {
            Category category = Mapper.Map<Category>(categoryViewModel);
            return TheUnitOfWork.Category.CheckCategoryExists(category);
        }
        #endregion

        #region pagination
        public int CountEntity()
        {
            return TheUnitOfWork.Category.CountEntity();
        }
        public IEnumerable<CategoryViewModel> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<List<CategoryViewModel>>(TheUnitOfWork.Category.GetPageRecords(pageSize, pageNumber));
        }
        #endregion
    }
}
