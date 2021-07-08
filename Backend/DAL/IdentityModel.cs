using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationUserIdentity : IdentityUser
    {
        // public string Id{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName 
        {
            get
            {
                return $"{FirstName} {LastName}";
            } 
        }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        [Required]
        public bool isDeleted { get; set; }
        public List<Payment> Payments { get; set; }
    }
    public class ApplicationUserStore : UserStore<ApplicationUserIdentity>
    {
       
        public ApplicationUserStore() : base(new ApplicationDBContext())
        {

        }
        public ApplicationUserStore(DbContext db) : base(db)
        {

        }
    }



    //public class ApplicationRoleManager : RoleManager<IdentityRole>
    //{
    //    public ApplicationRoleManager()
    //        : base(new RoleStore<IdentityRole>(new ApplicationDBContext()))
    //    {

    //    }
    //    public ApplicationRoleManager(DbContext db)
    //        : base(new RoleStore<IdentityRole>(db))
    //    {

    //    }
    //}
    //public class ApplicationUserManager : UserManager<ApplicationUserIdentity>
    //{
    //    public ApplicationUserManager() : base(new ApplicationUserStore())
    //    {

    //    }
    //    public ApplicationUserManager(DbContext db) : base(new ApplicationUserStore(db))
    //    {

    //    }

    //}
    public class ApplicationDBContext : IdentityDbContext<ApplicationUserIdentity>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder
                .UseSqlServer("Data Source=.;Initial Catalog=TestCore2;Integrated Security=True"
                , options => options.EnableRetryOnFailure());
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public ApplicationDBContext()
        {

        }
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public  DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Color> Colors { get; set; }

        public DbSet<ProductCart> ProductCarts { get; set; }
        public DbSet<ProductWishList> ProductWishLists { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        }

    
}
