using iShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Data.Configurations
{
    class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslations");
            builder.HasKey(x => x.ProductTranslationId);
            builder.Property(x => x.ProductTranslationId).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.Property(x => x.SeoAlias).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Details).HasMaxLength(500);


            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            //builder.HasData(x => x.Languages).WithMany(x => x.ProductTranslations).HasForeignKey(x => x.LanguageId);

            builder.HasOne(x => x.Products).WithMany(x => x.ProductTranslations).HasForeignKey(x => x.ProductId);

        }
    }
}
