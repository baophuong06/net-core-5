using iShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Data.Configurations
{
  public  class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(x => x.PromotionId);
            builder.Property(x => x.PromotionId).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired();
        }
    
    }
}
