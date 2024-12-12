using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }



        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Achieve> CheckoutAchieves { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           // Seed initial roles
            builder.Entity<PaymentMethod>().HasData(
                new PaymentMethod
                {
                    Id = Guid.NewGuid(),
                    Name = "Credit Card",
                }
            );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "99d02fb7-afd3-4d2b-9a98-8af4d140e4c4",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "9d38241f-41f5-48c6-b808-990ed95a6076",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        }

    }


}
