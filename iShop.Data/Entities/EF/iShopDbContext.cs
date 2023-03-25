using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using iShop.Data.Entities;
//using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Identity;
using iShop.Data.Entities.Extensions;
using iShop.Data.Configurations;
//using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace iShop.Data.Entities.EF
{
    public class iShopDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public iShopDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Connectstring

            //Congigure using Fluent API
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration()); 
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());
            modelBuilder.ApplyConfiguration(new SlideConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            //Data Seeding


            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            modelBuilder.Seed();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = "Server=ADMIN;Database=iShopping;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(conn);
           // optionsBuilder.ReplaceService<IQueryTranslationPostprocessorFactory, SqlServer2008QueryTranslationPostprocessorFactory>();
        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<AppConfig> Configs { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductInCategory> ProductInCategorys { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
       
    }

}  
