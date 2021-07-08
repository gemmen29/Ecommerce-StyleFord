using BL.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Dtos;
using DAL.Models;
using BL.Interfaces;
using AutoMapper;

namespace BL.AppServices
{
    public class ReviewsAppService: AppServiceBase
    {
        public ReviewsAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
           
        }
        public ReviewsViewModel SaveNewReview(Review review)
        {

            bool result = false;
            if (TheUnitOfWork.Review.Insert(review))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return (result)? Mapper.Map<ReviewsViewModel>(review): null;
        }
        public ReviewsViewModel UpdateReview(Review review) 
        {
            bool result = false;
            Review oldReview = TheUnitOfWork.Review.GetReviewById(review.ID);
            Mapper.Map(review, oldReview);
            TheUnitOfWork.Review.Update(oldReview);
            result = TheUnitOfWork.Commit() > new int();
            return (result) ? Mapper.Map<ReviewsViewModel>(oldReview) : null;
        }
        public bool DeleteReview(int id)
        {
            bool result = false;
            TheUnitOfWork.Review.Delete(id);
            result = TheUnitOfWork.Commit() > new int();
            return result;
        }

        public ReviewsViewModel GetUserReviewOnProduct(string userID, int productId)
        {
            Review review = TheUnitOfWork.Review
                .GetUserReviewOnProduct(userID, productId);
            return Mapper.Map<ReviewsViewModel>(review);
            
        }

        public double GetAverageRateForProduct(int productId)
        {
            return TheUnitOfWork.Review.GetAverageRateForProduct(productId);
        }
        #region pagination
        public int CountEntity(int productId)
        {
            return TheUnitOfWork.Review.CountProductReviews(productId);
        }
        public IEnumerable<ReviewsViewModel> GetPageRecords(int productId, int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<ReviewsViewModel>>(TheUnitOfWork.Review.GetReviewsPageRecords(productId, pageSize, pageNumber));
        }
        #endregion


        //public  bool AddReview(Review review)
        //{
        //    var result = false;
        //    //check if review exist or not 
        //    //if exist update it else add new 
        //    var  reviewSearched = TheUnitOfWork.Review.GetReview(review.UserID, review.ProductID);
        //    if (reviewSearched != null)
        //    {
        //        review.ID = reviewSearched.ID;
        //        result= UpdateReview(review);
        //    }     
        //    else
        //    {
        //        result= SaveNewReview(review);
        //    }

        //    return result ;
        //}

    }
}
