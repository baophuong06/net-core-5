using iShop.Data.Entities;
using iShop.Data.Entities.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Data.Configurations
{
    public class CategoriesConfiguration : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.CategoryId);

            builder.Property(x => x.CategoryId).UseIdentityColumn();


            builder.Property(x => x.Status).HasDefaultValue(status.Active);
        }
    }
  
    }

