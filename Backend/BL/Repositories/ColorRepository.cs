using BL.Bases;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
   public class ColorRepository:BaseRepository<Color>
    {

        private DbContext EC_DbContext;

        public ColorRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<Color> GetAllColors()
        {
            return GetAll().ToList();
        }

        public bool InsertColor(Color color)
        {
            return Insert(color);
        }
        public void UpdateColor(Color color)
        {
            Update(color);
        }
        public void DeleteColor(int id)
        {
            Delete(id);
        }

        public bool CheckColorExists(Color color)
        {
            return GetAny(l => l.ID== color.ID);
        }
        public Color GetColorById(int id)
        {
            return GetFirstOrDefault(l => l.ID == id);
        }
        #endregion
    }
}

