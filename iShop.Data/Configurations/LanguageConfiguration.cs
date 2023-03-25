using iShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Data.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages");

            builder.HasKey(x => x.LanguageId);

            builder.Property(x => x.LanguageId).IsRequired().IsUnicode(false).HasMaxLength(5);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
        }
    }
}
