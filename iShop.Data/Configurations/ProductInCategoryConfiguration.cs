using System;
using System.Collections.Generic;
using System.Text;
using iShop.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace iShop.Data.Configurations
{
   public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(x => new { x.CategoryInProductId, x.ProductInCategoryId });
            builder.ToTable("ProductInCategories");
            builder.HasOne(t => t.Products).WithMany(pc => pc.ProductInCategories)
               .HasForeignKey(pc => pc.CategoryInProductId);

            builder.HasOne(t => t.Category).WithMany(pc => pc.ProductInCategories)
              .HasForeignKey(pc => pc.ProductInCategoryId);
        }
    }
    
}
