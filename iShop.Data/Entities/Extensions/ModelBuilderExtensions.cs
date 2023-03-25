using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Data.Entities.Extensions
{
    //Data Seeding
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData( new AppConfig(){Key="HomeTitlte",Value="This is Home Page of Ngoc"},
                new AppConfig() { Key = "HomeKeyword", Value = "This is Keywork of Page" },
                new AppConfig() { Key = "HomeDescription", Value = "This is Description of Page" });

            modelBuilder.Entity<Language>().HasData
                (new Language() { LanguageId="vi", Name="Tiếng Việt",IsDefault=true},
                new Language() { LanguageId="es", Name="English", IsDefault=false});

            modelBuilder.Entity<Slide>().HasData(new Slide() {SlideId=1,Name = "Second Thumbnail label",Description= "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus." +
                " Nullam id dolor id nibh ultricies vehicula ut id elit." , SortOrder=1,Url="#",Image= "themes/images/carousel/1.png",Status=enums.status.Active
            },

              new Slide() {
                  SlideId = 2,
                  Name = "Second Thumbnail label",
                  Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus .Nullam id dolor id nibh ultricies vehicula ut id elit.",
                  SortOrder = 2,
                  Url = "#",
                  Image = "themes/images/carousel/2.png",
                  Status = enums.status.Active
              },
              new Slide() {
                  SlideId = 3,
                  Name = "Second Thumbnail label",
                  Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus." +
                " Nullam id dolor id nibh ultricies vehicula ut id elit.",
                  SortOrder = 3,
                  Url = "#",
                  Image = "themes/images/carousel/3.png",
                  Status = enums.status.Active
              },
              new Slide() {
                  SlideId = 4,
                  Name = "Second Thumbnail label",
                  Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus." +
                " Nullam id dolor id nibh ultricies vehicula ut id elit.",
                  SortOrder = 4,
                  Url = "#",
                  Image = "themes/images/carousel/4.png",
                  Status = enums.status.Active,
              });

            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "nguyenngoc@gmail.com",
                NormalizedEmail = "nguyenngoc@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Ngoc",
                LastName = "Nguyen",
                Dob = new DateTime(2020, 01, 31)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid> {
                RoleId = roleId,
                UserId = adminId
            });


            modelBuilder.Entity<Categories>().HasData(new Categories() {
                CategoryId = 1,
                IsShowOnHome=true,
                ParentId = null,
                SortOrder = 1,
                Status = enums.status.Active,
                
            },

           new Categories() {
                CategoryId = 2,
                IsShowOnHome=true,
                ParentId = null,
                SortOrder = 2,
                Status = enums.status.Active,

            }

              );
            modelBuilder.Entity<CategoryTranslation>().HasData(
                    new CategoryTranslation() {CategoryTranslationId=1, CategoryId = 1, Name = "Ao nam", LanguageId = "vi", SeoAlias = "ao-nam" ,SeoDescription="ao-nam"},
                    new CategoryTranslation() { CategoryTranslationId = 2, CategoryId = 1, Name = "T-mem Shirt", LanguageId = "es", SeoAlias = "T-men Shirt", SeoDescription = "T-men Shirt" },
                    new CategoryTranslation() { CategoryTranslationId = 3, CategoryId = 2, Name = "Quan nu", LanguageId = "vi", SeoAlias = "quan-nu", SeoDescription = "quan-nu" },
                    new CategoryTranslation() { CategoryTranslationId = 4, CategoryId = 2, Name = "Jean-Woman", LanguageId = "es", SeoAlias = "Jean-Woman", SeoDescription = "Jean-Woman" }
                   );
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId=1, 
                    Name="Quan Jean Nu",
                    Price=500, 
                    DateCreate=DateTime.Now,
                    OriginalPrice=200,Stock=0,
                    ViewCount=0,
                   
                },
                 new Product {
                     ProductId = 2,
                     Name = "Quan Jean Nam",
                     Price = 700,
                     DateCreate = DateTime.Now,
                     OriginalPrice = 400,
                     Stock = 0,
                     ViewCount = 0,
                   
                 }
                );
            modelBuilder.Entity<ProductTranslation>().HasData(
                        new ProductTranslation() {
                            ProductId=1,
                            ProductTranslationId=1,
                            LanguageId = "vi",
                            Name = "Quan Jean Nu",
                            SeoAlias = "quan-nu",
                            SeoTitle = "San pham quan thoi trang nu",
                            SeoDescription = "San pham quan thoi trang nu",
                            Details = "Mo ta san pham",
                            Description = ""
                        },
                        new ProductTranslation() {
                            ProductId=1,
                            ProductTranslationId=2,
                            LanguageId = "es",
                            Name = "Quan Jean Nu",
                            SeoAlias = "woman jean",
                            SeoTitle = "San pham quan thoi trang nu",
                            SeoDescription = "San pham quan thoi trang nu",
                            Details = "Mo ta san pham",
                            Description = ""
                        },
                         new ProductTranslation() {
                             ProductId=2,
                             ProductTranslationId=3,
                             LanguageId = "vi",
                             Name = "Quan Jean Nam",
                             SeoAlias = "quan-nam",
                             SeoTitle = "San pham quan thoi trang nam",
                             SeoDescription = "San pham quan thoi trang nam",
                             Details = "Mo ta san pham",
                             Description = ""
                         },
                        new ProductTranslation() {
                            ProductId=2,
                            ProductTranslationId=4,
                            LanguageId = "es",
                            Name = "Quan Jean Nam",
                            SeoAlias = "men jean",
                            SeoTitle = "San pham quan thoi trang nam",
                            SeoDescription = "San pham quan thoi trang nam",
                            Details = "Mo ta san pham",
                            Description = ""
                        }
                    
                    );
            modelBuilder.Entity<ProductInCategory>().HasData(new ProductInCategory() { ProductInCategoryId = 1, CategoryInProductId = 1 });
        }
    }
}
