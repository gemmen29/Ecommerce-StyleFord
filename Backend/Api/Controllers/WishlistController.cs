using BL.AppServices;
using BL.Dtos;
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
    public class WishlistController : ControllerBase
    {
        ProductWishListAppService _productWishListAppService;
        ProductAppService _productAppService;
        WishlistAppService _wishlistAppService;
        IHttpContextAccessor _httpContextAccessor;
        public WishlistController(ProductWishListAppService productWishListAppService,
                                  ProductAppService productAppService,
                                  WishlistAppService wishlistAppService,
                                  IHttpContextAccessor httpContextAccessor)

        {
            this._productAppService = productAppService;
            this._wishlistAppService = wishlistAppService;
            this._productWishListAppService = productWishListAppService;
            this._httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult getUserWishList()
        {
            //get all products in specfic wishlist
            //firs get cart id of logged user
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var userID = "88d2bf8e-a1ec-41ee-a0da-22d9e25ca54b";
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var wishListID = _wishlistAppService.GetAllWishlists().Where(w => w.ID == userID)
            //                                               .Select(w => w.ID).FirstOrDefault();
            var productIDs = _productWishListAppService.GetAllProductWishList().Where(w => w.wishlistId == userID).Select(wpr => wpr.productId);
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            foreach (var proID in productIDs)
            {
                var product = _productAppService.GetProduct(proID);
                productViewModels.Add(product);
            }
            return Ok(productViewModels);
        }
        //[HttpPut]
        [HttpPost("{productID}")]
        //make it as httpPut because we will update on user wishlist
        public IActionResult AddProductToWishList(int productID)
        {
            //var userID = "88d2bf8e-a1ec-41ee-a0da-22d9e25ca54b";
            //get wishlist of current logged user
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var wishListID = _wishlistAppService.GetAllWishlists().Where(w => w.ID == userID)
            //                                               .Select(w => w.ID).FirstOrDefault();
            var productWishListViewModel = new ProductWishListViewModel() { wishlistId = userID, productId = productID };
            //var isExistingProductWishListViewModel = _productWishListAppService.GetAllProductWishList()
            //                                    .FirstOrDefault(w => w.wishlistId == productWishListViewModel.wishlistId && w.productId == productWishListViewModel.productId);
            var isExistingProductWishListViewModel = _productWishListAppService.CheckIfProductExistsInWishlist(userID, productID);
            if (isExistingProductWishListViewModel == false)
            {
                _productWishListAppService.SaveNewProductWishlist(productWishListViewModel);
                return Ok();
            }
            return BadRequest("this product already exist in wishList");
        }
        //[HttpDelete]
        [HttpDelete("{productID}")]
        public IActionResult DeleteFromWishList(int productID)
        {
            //var userID = "2be43fb0-6f7f-4662-893b-66bd033beda6";
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isExistingProductWishListViewModel = _productWishListAppService.CheckIfProductExistsInWishlist(userID, productID);
            if (isExistingProductWishListViewModel == true)
            {
                _productWishListAppService.DeleteProductWishList(_productWishListAppService.GetProductWishlistID(userID,productID));
                return Ok();
            }
            return BadRequest("this product doesn't exist in wishList");
            //var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var wishListID = _wishlistAppService.GetAllWishlists().Where(w => w.ID == userID)
            //                                               .Select(w => w.ID).FirstOrDefault();
            //var productWishlistViewModel = new ProductWishListViewModel() { wishlistId = userID, productId = productID };
            //var deletedProductWishList = _productWishListAppService.GetAllProductWishList()
            //                                     .FirstOrDefault(w => w.wishlistId == productWishlistViewModel.wishlistId && w.productId == productWishlistViewModel.productId);

            //var isDeleted = _productWishListAppService.DeleteProductWishList(deletedProductWishList.ID);
            //if (isDeleted)
            //    return Content("Deleted succfully");
            //return Content("Error Occur In Deletion");


        }
    }
}
