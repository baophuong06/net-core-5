using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace iShop.Data.Entities.EF
{
   public class iShopDbContextFactory : IDesignTimeDbContextFactory<iShopDbContext>
    {
        public iShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<iShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new iShopDbContext(optionsBuilder.Options);
        }
    }
}
