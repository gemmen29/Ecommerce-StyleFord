using AutoMapper;
using BL.Bases;
using BL.Dtos;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class ColorAppService : AppServiceBase
    {
        public ColorAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        #region CURD

        public IEnumerable<ColorDTO> GetAllColors()
        {

            return Mapper.Map<IEnumerable<ColorDTO>>(TheUnitOfWork.Color.GetAllColors());
        }
        public ColorDTO GetColor(int id)
        {
            return Mapper.Map<ColorDTO>(TheUnitOfWork.Color.GetById(id));
        }



        public bool SaveNewColor(ColorDTO ColoDTO)
        {
            if (ColoDTO == null)

                throw new ArgumentNullException();

            bool result = false;
            var color = Mapper.Map<Color>(ColoDTO);
            if (TheUnitOfWork.Color.Insert(color))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateColor(ColorDTO ColoDTO)
        {
            var color = Mapper.Map<Color>(ColoDTO);
            TheUnitOfWork.Color.Update(color);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteColor(int id)
        {
            bool result = false;

            TheUnitOfWork.Color.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckColorExists(ColorDTO ColoDTO)
        {
            Color color = Mapper.Map<Color>(ColoDTO);
            return TheUnitOfWork.Color.CheckColorExists(color);
        }
        #endregion

        #region pagination
        public int CountEntity()
        {
            return TheUnitOfWork.Color.CountEntity();
        }
        public IEnumerable<ColorDTO> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<List<ColorDTO>>(TheUnitOfWork.Color.GetPageRecords(pageSize, pageNumber));
        }
        #endregion
    }
}
