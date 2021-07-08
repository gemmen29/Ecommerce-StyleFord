using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bases;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public class ReviewsRepository : BaseRepository<Review>
    {
        private DbContext EC_DbContext;

        public ReviewsRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }

        public bool InsertReview(Review review)
        {
            return Insert(review);
        }

        internal int CountProductReviews(int productId)
        {
            return DbSet.Where(p => p.ProductID == productId).Count();
        }

        internal IEnumerable<Review> GetReviewsPageRecords(int productId, int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return DbSet
                .Where(r=>r.ProductID == productId)
                .Include(r=>r.User)
                .OrderByDescending(r=>r.ID)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToList();
        }

        internal double GetAverageRateForProduct(int productId)
        {
            double ratingAverage = DbSet.Where(r => r.ProductID == productId)
                .Select(r => r.Rating).Average();
            return ratingAverage;
        }
        public Review GetReviewById(int id)
        {
            return DbSet.Include(r => r.User).FirstOrDefault(r => r.ID == id);
        }
        internal Review GetUserReviewOnProduct(string userID, int productId)
        {
            return DbSet
                .Include(r => r.User)
                .FirstOrDefault(r => r.UserID == userID && r.ProductID == productId);
        }
    }
}
