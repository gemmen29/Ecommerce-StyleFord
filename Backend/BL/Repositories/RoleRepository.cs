using BL.Bases;
using BL.StaticClasses;
using BL.Dtos;
using DAL;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class RoleRepository : BaseRepository<IdentityRole>
    {
        RoleManager<IdentityRole> manager;
     
        public RoleRepository(DbContext db, RoleManager<IdentityRole> manager) :base(db)
        {
            this.manager = manager;
         
         
        }
        public IdentityRole GetRoleByID(string id)
        {
            return GetFirstOrDefault(r => r.Id == id);
        }

        public async Task CreateRoles()
        {
            
            if (!await manager.RoleExistsAsync(UserRoles.Admin))
                await manager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await manager.RoleExistsAsync(UserRoles.User))
                await manager.CreateAsync(new IdentityRole(UserRoles.User));

        }
        public IdentityResult Create(string role)
        {
            return manager.CreateAsync(new IdentityRole(role)).Result;
           
        }
        public async Task<IdentityResult> UpdateRole(IdentityRole role)
        {
            var identityRole = await manager.FindByIdAsync(role.Id);
            identityRole.Name = role.Name;
           return await manager.UpdateAsync(identityRole);

     
        }
        public async void DeleteRole(string id)
        {
            var identityRole = await manager.FindByIdAsync(id);
       
            await manager.DeleteAsync(identityRole);
        }
        public List<IdentityRole> getAllRoles()
        {
            // return GetAll().Include(r=>r.Users).ToList();
            return GetAll().ToList();
        }
        //public List<IdentityRole> getRole(string id)
        //{
        //    return GetAll().Where(r=>r.Id ==id).Include(r=>r.Users).ToList();
        //}

    }
}
