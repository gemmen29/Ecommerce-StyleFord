using BL.Bases;
using BL.Interfaces;
using BL.StaticClasses;
using BL.Dtos;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BL.AppServices
{
    public class AccountAppService : AppServiceBase
    {
        IConfiguration _configuration;
        CartAppService _cartAppService;
        WishlistAppService _wishlistAppService;

        public AccountAppService(IUnitOfWork theUnitOfWork,IConfiguration configuration,
            CartAppService cartAppService, WishlistAppService wishlistAppService, IMapper mapper) : base(theUnitOfWork, mapper)
        {
            this._configuration = configuration;
            this._cartAppService = cartAppService;
            this._wishlistAppService = wishlistAppService;
        }
        private void CreateUserCartAndWishlist(string userId)
        {
            _wishlistAppService.CreateUserWishlist(userId);
            _cartAppService.CreateUserCart(userId);
        }
        public List<RegisterViewodel> GetAllAccounts()
        {
            return Mapper.Map<List<RegisterViewodel>>(TheUnitOfWork.Account.GetAllAccounts().Where(ac => ac.isDeleted == false));
        }
        public RegisterViewodel GetAccountById(string id)
        {
            if (id == null)
                throw new ArgumentNullException();
            return Mapper.Map<RegisterViewodel>(TheUnitOfWork.Account.GetAccountById(id));

        }

        public bool DeleteAccount(string id)
        {
            if (id == null)
                throw new ArgumentNullException();
            bool result = false;
            ApplicationUserIdentity user = TheUnitOfWork.Account.GetAccountById(id);
            user.isDeleted = true;
            TheUnitOfWork.Account.Update(user);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public async Task<ApplicationUserIdentity> Find(string name, string password)
        {
            ApplicationUserIdentity user = await TheUnitOfWork.Account.Find(name, password);

            if (user != null && user.isDeleted == false)
                return user;
            return null;
        }
        public async Task<ApplicationUserIdentity> FindByName(string userName)
        {
            ApplicationUserIdentity user = await TheUnitOfWork.Account.FindByName(userName);

            if (user != null && user.isDeleted == false)
                return user;
            return null;
        }
        public async Task<IdentityResult> Register(RegisterViewodel user)
        {
            bool isExist = await checkUserNameExist(user.UserName);
            if(isExist)
                return IdentityResult.Failed(new IdentityError
                { Code = "error", Description = "user name already exist" });

            ApplicationUserIdentity identityUser = Mapper.Map<RegisterViewodel, ApplicationUserIdentity>(user);
            var result = await TheUnitOfWork.Account.Register(identityUser);
            // create user cart and wishlist 
            if (result.Succeeded)
            {
                CreateUserCartAndWishlist(identityUser.Id);
            }
            return result;
        }
        public async Task<IdentityResult> AssignToRole(string userid, string rolename)
        {
            if (userid == null || rolename == null)
                return null;
            return await TheUnitOfWork.Account.AssignToRole(userid, rolename);
        }
        public async Task<bool> UpdatePassword(string userID, string newPassword)
        {
            //    ApplicationUserIdentity identityUser = TheUnitOfWork.Account.FindById(user.Id);

            //    Mapper.Map(user, identityUser);

            //    return TheUnitOfWork.Account.UpdateAccount(identityUser);


            ApplicationUserIdentity identityUser = await TheUnitOfWork.Account.FindById(userID);
            identityUser.PasswordHash = newPassword;
            return await TheUnitOfWork.Account.updatePassword(identityUser);

        }

        public async Task<bool> Update(RegisterViewodel user)
        {
            //    ApplicationUserIdentity identityUser = TheUnitOfWork.Account.FindById(user.Id);

            //    Mapper.Map(user, identityUser);

            //    return TheUnitOfWork.Account.UpdateAccount(identityUser);


            ApplicationUserIdentity identityUser = await TheUnitOfWork.Account.FindById(user.Id);
            var oldPassword = identityUser.PasswordHash;
            Mapper.Map(user, identityUser);
            identityUser.PasswordHash = oldPassword;
            return await TheUnitOfWork.Account.UpdateAccount(identityUser);

        }
        public async Task<bool> checkUserNameExist(string userName)
        {
            var user = await TheUnitOfWork.Account.FindByName(userName);
            return user == null ? false : true;
        }
        public async Task<IEnumerable<string>> GetUserRoles (ApplicationUserIdentity user)
        {
            return await TheUnitOfWork.Account.GetUserRoles(user);
        }
       public async Task<dynamic> CreateToken(ApplicationUserIdentity user)
        {
            var userRoles = await GetUserRoles(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim("role",userRoles.FirstOrDefault()),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            } ;

           
        }

        public async Task CreateFirstAdmin()
        {
            var firstAdmin = new RegisterViewodel()
            {
                Id = null,
                Email = "test@gmail.com",
                FirstName = "first",
                LastName = "user",
                UserName = "admin",
                PasswordHash = "@Admin12345",
                BirthDate = DateTime.Now,
                isDeleted = false
            };
            Register(firstAdmin).Wait();
            ApplicationUserIdentity foundedAdmin = await FindByName(firstAdmin.UserName);
            if(foundedAdmin != null)
                AssignToRole(foundedAdmin.Id, UserRoles.Admin).Wait();
        }
        #region pagination
        public int CountEntity()
        {
            return TheUnitOfWork.Account.CountEntity();
        }
        public IEnumerable<RegisterViewodel> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<List<RegisterViewodel>>(TheUnitOfWork.Account.GetPageRecords(pageSize, pageNumber).Where(ac => ac.isDeleted == false));
        }
        #endregion

    }
}
