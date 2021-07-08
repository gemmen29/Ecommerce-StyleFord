using BL.AppServices;
using BL.Dtos;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        IHttpContextAccessor _httpContextAccessor;
        ReviewsAppService _reviewsAppService;
        public ReviewController(ReviewsAppService reviewsAppService,IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._reviewsAppService = reviewsAppService;
        }
        [HttpGet("{productId}")]
        public IActionResult GetUserReviewOnProduct(int productId)
        {
            string userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ReviewsViewModel userReview = _reviewsAppService.GetUserReviewOnProduct(userID, productId);
            return Ok(userReview);
        }
        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                review.UserID = userID;
                ReviewsViewModel addedReview = _reviewsAppService.SaveNewReview(review);

                return Created("created", addedReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{productId}")]
        public IActionResult Edit(int productId,Review review)
        {
            if(review.ProductID != productId)
            {
                return BadRequest("ids not matched");
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                review.UserID = userID;
                ReviewsViewModel updatedReview = _reviewsAppService.UpdateReview(review);
                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _reviewsAppService.DeleteReview(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("averageRate/{productId}")]
        public IActionResult ProductAverageRate(int productId)
        {
            return Ok(_reviewsAppService.GetAverageRateForProduct(productId));
        }

        [HttpGet("count/{productId}")]
        public IActionResult ReviewsCount(int productId)
        {
            return Ok(_reviewsAppService.CountEntity(productId));
        }
        [HttpGet("{productId}/{pageSize}/{pageNumber}")]
        public IActionResult GetReviewsByPage(int productId ,int pageSize, int pageNumber)
        {
            return Ok(_reviewsAppService.GetPageRecords(productId,pageSize, pageNumber));
        }
    }
}
