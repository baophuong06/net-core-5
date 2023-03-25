
using iShop.Data.Entities;
using iShop.Data.Entities.EF;
using iShop.Web.Unitity.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using iShop.Web.ViewModel.Common;
using iShop.Web.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using iShop.Application.Domain.Common;
using System.IO;
using iShop.Web.ViewModel.Catalog.Products.Public;
using iShop.Web.ViewModel.Catalog.Products.Manager;
using System.Web.Mvc;

namespace iShop.Application.Domain.Catalog.Products
{
    public class ManagerProductService : IManagerProductService
    {
        private readonly iShopDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public ManagerProductService(iShopDbContext context,IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int productId,  ProductImageCreateRequest request)
        {
            var productImage = new ProductImage() {
                Caption = request.Caption,
                DateCreated=DateTime.Now,
                IsDefault=request.IsDefault,
                ProductId=productId,
                SortOrder=request.SortOrder
            };
            if(request.ImageFile!=null) {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.ProductId;
        }

        public async Task AddViewCount(int productId)
        {
            var product =await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
            
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product() 
            {
                Price=request.Price,
                Name=request.Name,
                OriginalPrice=request.OriginalPrice,
                Stock=request.Stock,
                ViewCount=0,
                DateCreate=DateTime.Now,
                //ProductInCategories=new List<ProductInCategory>() { 
                //new ProductInCategory() {
                //    CategoryInProductId=request.CategoryId
                //}
                //},
                ProductTranslations=new List<ProductTranslation>() {
                    new ProductTranslation() {
                        Name=request.Name,
                        Description=request.Description,
                        Details=request.Details,
                        SeoAlias=request.SeoAlias,
                        SeoDescription=request.SeoDescription,
                        SeoTitle=request.SeoTitle,
                       //Languages=new List<Language>() {
                       //    new Language() {
                       //        CategoryTranslations=new List<CategoryTranslation>() {
                       //            new CategoryTranslation() {
                       //                CategoryId=request.CategoryId
                       //            }
                       //        }
                       //    }
                       //},
                        LanguageId=request.LanguageId
                    }
                }
            };
            //save Image
            if(request.ThumnailImage!=null) {
                product.ProductImages = new List<ProductImage>() {

                    new ProductImage() {
                        Caption="Thumail Image",
                        DateCreated=DateTime.Now,
                        FileSize=request.ThumnailImage.Length,
                        ImagePath=await this.SaveFile(request.ThumnailImage),
                        IsDefault=true,
                        SortOrder=1
                    }
                };
            }
            _context.Products.Add(product);
          await _context.SaveChangesAsync();
            return product.ProductId;

        }

       
        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new IShopException($"Can not found product:{productId}");
            var images = _context.ProductImages.Where(x => x.ProductId == productId);
            foreach(var image in images) {
                await _storageService.DeleteFileAsnyc(image.ImagePath);
            }
            _context.Products.Remove(product);
            
          return  await _context.SaveChangesAsync();
        }



        //public Task<PageViewModel<ProductViewModel>> GetAllPaging(string keyword, int pageSize, int pageIndex)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<PageList<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            //select join
            //var query = from p in _context.Products
            //            join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
            //            join pic in _context.ProductInCategorys on p.ProductId equals pic.ProductInCategoryId
            //            join c in _context.Categories on pic.CategoryInProductId equals c.CategoryId
            //            join pim in _context.ProductImages on p.ProductId equals pim.ProductId
            //            select new { p, pt, pic,pim };
            ////filter
            //if (!string.IsNullOrEmpty(request.keyword))
            //    query = query.Where(x => x.pt.Name.Contains(request.keyword));
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategorys on p.ProductId equals pic.ProductInCategoryId
                        join c in _context.Categories on pic.CategoryInProductId equals c.CategoryId
                        join cl in _context.CategoryTranslations on c.CategoryId equals cl.CategoryId
                        join pim in _context.ProductImages on p.ProductId equals pim.ProductId
                       // where pt.LanguageId == languageId
                        select new { p, pt, pic ,pim};
            //filter
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0) {
                query = query.Where(p => p.pic.CategoryInProductId == request.CategoryId);
            }
            //if (request.categoryIds.Count > 0)
            //    query = query.Where(p => request.categoryIds.Contains(p.pic.CategoryInProductId));
            //paging
            //int totalRow = await query.CountAsync();
            var data =await query.OrderBy(x=>x.p.Name)
                .Select(x => new ProductViewModel() {
                    ProductId = x.p.ProductId,
                    Name = x.pt.Name,
                    DateCreate = x.p.DateCreate,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    ViewCount = x.p.ViewCount,
                    Stock = x.p.Stock,
                    ThumbnailImage=x.pim.ImagePath
                }).ToListAsync();
            //Selelct
            //var pageResult = new PageResult<ProductViewModel>() {
            //    TotalRecord = totalRow,
            //    Items = data,
            //};
            
            return PageList<ProductViewModel>.CreatePageList(data,request.PageIndex,request.PageSize);

        }

        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productTranlation = await _context.ProductTranslations
                .FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);
            var productVm = new ProductViewModel() {
                ProductId=product.ProductId,
                DateCreate=product.DateCreate,
                Description=productTranlation!=null ? productTranlation.Description : null,
                LanguageId=productTranlation.LanguageId,
                Details= productTranlation != null ? productTranlation.Details : null,
                Name= productTranlation != null ? productTranlation.Name : null,
                Price=product.Price,
                OriginalPrice=product.OriginalPrice,
                SeoAlias= productTranlation != null ? productTranlation.SeoAlias : null,
                SeoDescription= productTranlation != null ? productTranlation.SeoDescription : null,
                SeoTitle= productTranlation != null ? productTranlation.SeoTitle : null,
                Stock=product.Stock,
                ViewCount=product.ViewCount
                
            };
            return productVm;
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null) throw new IShopException($"Cannot find image:");

            var imageVm = new ProductImageViewModel() {
                Caption=image.Caption,
                DateCreated=image.DateCreated,
                FileSize=image.FileSize,
                Id=image.ProductImageId,
                ImagePath=image.ImagePath,
                IsDefault=image.IsDefault,
                ProductId=image.ProductId,
                SortOrder=image.SortOrder
                            };
            return imageVm;
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId)
                 .Select(a => new ProductImageViewModel() {
                    Caption=a.Caption,
                    DateCreated=a.DateCreated,
                    FileSize=a.FileSize,
                    Id=a.ProductId,
                     ImagePath = a.ImagePath,
                     IsDefault = a.IsDefault,
                     ProductId = a.ProductId,
                     SortOrder = a.SortOrder

                 }).ToListAsync();
        }

        public async Task<PageResult<ProductViewModel>> GetAllCategoryId(GetPagingProductRequest request, string languageId)
        {
            //select jion
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategorys on p.ProductId equals pic.ProductInCategoryId
                        join c in _context.Categories on pic.CategoryInProductId equals c.CategoryId
                        join cl in _context.CategoryTranslations on c.CategoryId equals cl.CategoryId
                        where pt.LanguageId == languageId
                        select new { p, pt, pic };
            //filter
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0) {
                query = query.Where(p => p.pic.CategoryInProductId == request.CategoryId);
            }


            //paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel() {
                    ProductId = x.p.ProductId,
                    Name = x.pt.Name,
                    DateCreate = x.p.DateCreate,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    ViewCount = x.p.ViewCount,
                    Stock = x.p.Stock,

                }).ToListAsync();
            //Selelct
            var pageResult = new PageResult<ProductViewModel>() {
                TotalRecord = totalRow,
                Items = data,
            };
            return pageResult;
        }
    
    public async Task<int> RemoveImage(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null) throw new IShopException($"Cannot find image :{imageId}");
            _context.ProductImages.Remove(image);
            return await _context.SaveChangesAsync();

        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranlation =await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id
            && x.LanguageId == request.LanguageId);
            if (product == null || productTranlation == null) throw new IShopException($"Not found product :{request.Id}");
            productTranlation.Name = request.Name;
            productTranlation.SeoAlias = request.SeoAlias;
            productTranlation.SeoDescription = request.SeoDescription;
            productTranlation.SeoTitle = request.SeoTitle;
            productTranlation.Description = request.Description;
            productTranlation.Details = request.Details;

            //save image
            if(request.ThumnailImage!=null) {
                var thumnailImage = await _context.ProductImages.FirstOrDefaultAsync(x => x.IsDefault == true && x.ProductId == request.Id);
                if(thumnailImage!=null) {
                    thumnailImage.FileSize = request.ThumnailImage.Length;
                    thumnailImage.ImagePath = await this.SaveFile(request.ThumnailImage);
                    _context.ProductImages.Update(thumnailImage);
                }
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null) throw new IShopException($"Cannot find image with id:{imageId}");
            if(request.ImageFile!=null) {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new IShopException($"Cannot found product :{productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new IShopException($"Cannot found product :{productId}");
            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsnyc(file.OpenReadStream(), fileName);
             return "/"+ USER_CONTENT_FOLDER_NAME +"/"+ fileName;
           // return fileName;
        }

        public async Task<PageList<ProductViewModel>> GetAllPagingSearchProduct(GetProductPagingRequest request)
        {
            //select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pi in _context.ProductImages on p.ProductId equals pi.ProductId
                        //join pic in _context.ProductInCategorys on p.ProductId equals pic.ProductInCategoryId
                        //join c in _context.Categories on pic.CategoryInProductId equals c.CategoryId
                        select new { p, pt, pi };
            if(!string.IsNullOrEmpty(request.keyword)) {
                query = query.Where(x => x.p.Name.Contains(request.keyword)|| x.pt.Description.Contains(request.keyword));
            }
           
            //if (request.categoryIds.Count > 0)
            //    query = query.Where(p => request.categoryIds.Contains(p.pic.CategoryInProductId));
            var product = await query.OrderBy(a => a.pt.Name)
                .Select(a => new ProductViewModel() {
                    ProductId = a.p.ProductId,
                    Details=a.pt.Details,
                    Name = a.pt.Name,
                    Description = a.pt.Description,
                    Price = a.p.Price,
                    OriginalPrice=a.p.OriginalPrice,
                    DateCreate=a.p.DateCreate,
                    SeoDescription=a.pt.SeoDescription,
                    SeoTitle=a.p.SeoAlias,
                    Stock=a.p.Stock,
                    LanguageId = a.pt.LanguageId,
                    SeoAlias = a.pt.SeoAlias
                }).ToListAsync();
            return PageList<ProductViewModel>.CreatePageList(product, request.PageIndex, request.PageSize);
        }

        public async Task<List<ProductViewModel>> GetAllCategoryById()//CategoryList category, int id)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategorys on p.ProductId equals pic.ProductInCategoryId
                        join c in _context.Categories on pic.CategoryInProductId equals c.CategoryId
                        join ct in _context.CategoryTranslations on c.CategoryId equals ct.CategoryId
                       
                        select new { p, pt, pic,c,ct };
            var product =await query//.Include(a => a.pt.Languages).Include(a => a.pic.Category)
                .Select(a=>
                 new ProductViewModel() {
                     Name = a.p.Name,
                     Price = a.p.Price,
                     LanguageId = a.pt.LanguageId,
                     CategoryName = a.ct.Name
                 }
                ).ToListAsync();

            return new List<ProductViewModel>(product);
           
        }

        public async Task<IEnumerable<SelectListItem>> GetLanguage()
        {
            var language =await _context.Languages.AsNoTracking()
                   .OrderBy(x => x.Name)
                   .Select(a => new SelectListItem {
                       Value = a.LanguageId.ToString(),
                       Text = a.Name
                   }).ToListAsync();
            var langt = new SelectListItem() {
                Value = null,
                Text = "--- select country ---"
            };
            language.Insert(0, langt);
            return new SelectList(language, "Value", "Text");
        }

        public async Task<IEnumerable<SelectListItem>> GetCategory(string languageID)
        {
            if(!String.IsNullOrWhiteSpace(languageID)) {
                var query =await _context.CategoryTranslations.AsNoTracking()
                              .OrderBy(x => x.Name)
                              .Where(x => x.LanguageId == languageID)
                              .Select(a => new SelectListItem {
                                  Value = a.CategoryId.ToString(),
                                  Text = a.Name
                              }).ToListAsync();

                return new SelectList(query, "Value", "Text");
            }
            return null;
           
        }

        public Task<int> GetAmount()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductViewModel>> GetFeatureProducts()
        {
            //select jion
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        //join pic in _context.ProductInCategorys on p.ProductId equals pic.ProductInCategoryId
                        join pi in _context.ProductImages on p.ProductId equals pi.ProductId
                        //join c in _context.Categories on pic.CategoryInProductId equals c.CategoryId
                        //join cl in _context.CategoryTranslations on c.CategoryId equals cl.CategoryId
                        where pi==null || pi.IsDefault==true
                        select new { p, pt, pi};
            //filter
            //if (request.CategoryId.HasValue && request.CategoryId.Value > 0) {
            //    query = query.Where(p => p.pic.CategoryInProductId == request.CategoryId);
            //}


            //paging
            // int totalRow = await query.CountAsync();
           // take = 4;
            var data = await query
                .OrderByDescending(x => x.p.DateCreate)
               // .Take(take)
                .Select(x => new ProductViewModel() {
                    ProductId = x.p.ProductId,
                    Name = x.pt.Name,
                    DateCreate = x.p.DateCreate,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    ViewCount = x.p.ViewCount,
                    Stock = x.p.Stock,
                    ThumbnailImage=x.pi.ImagePath
                }).ToListAsync();
           return new List<ProductViewModel>(data);
        }

        public async Task<List<ProductViewModel>> GetLatedProducts(int take)
        {
            //select jion
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        //join pic in _context.ProductInCategorys on p.ProductId equals pic.ProductInCategoryId
                        join pi in _context.ProductImages on p.ProductId equals pi.ProductId
                        //join c in _context.Categories on pic.CategoryInProductId equals c.CategoryId
                        //join cl in _context.CategoryTranslations on c.CategoryId equals cl.CategoryId
                        //where p.IsFeatured==true
                        select new { p, pt, pi };
            //filter
            //if (request.CategoryId.HasValue && request.CategoryId.Value > 0) {
            //    query = query.Where(p => p.pic.CategoryInProductId == request.CategoryId);
            //}


            //paging
            // int totalRow = await query.CountAsync();
            // take = 4;
            var data = await query
                .OrderByDescending(x => x.p.DateCreate)
                .Take(take)
                .Select(x => new ProductViewModel() {
                    ProductId = x.p.ProductId,
                    Name = x.pt.Name,
                    DateCreate = x.p.DateCreate,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    ViewCount = x.p.ViewCount,
                    Stock = x.p.Stock,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();
            return new List<ProductViewModel>(data);
        }

        public async Task<List<ProductViewModel>> GetFeatureProduct(string languageId, int take)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategorys on p.ProductId equals pic.ProductInCategoryId
                        join pi in _context.ProductImages on p.ProductId equals pi.ProductId
                        join c in _context.Categories on pic.CategoryInProductId equals c.CategoryId
                        //join cl in _context.CategoryTranslations on c.CategoryId equals cl.CategoryId
                        where pt.LanguageId==languageId
                        select new { p, pt, pic,pi };
            //filter
            //if (request.CategoryId.HasValue && request.CategoryId.Value > 0) {
            //    query = query.Where(p => p.pic.CategoryInProductId == request.CategoryId);
            //}


            //paging
            // int totalRow = await query.CountAsync();
            // take = 4;
            var data = await query
                .OrderByDescending(x => x.p.DateCreate)
                 .Take(take)
                .Select(x => new ProductViewModel() {
                    ProductId = x.p.ProductId,
                    Name = x.pt.Name,
                    DateCreate = x.p.DateCreate,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    ViewCount = x.p.ViewCount,
                    Stock = x.p.Stock,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();
            return new List<ProductViewModel>(data);
        }

        public Task<List<ProductViewModel>> GetLatedProduct(int take, string languageId)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<ProductEditViewModel>> CreateCategory()
        //{
        //    var product = new Product();
        //    var d = new ProductEditViewModel();
        //    product.ProductId = d.ProductId;
        //    //product.Name=d.
        //    product.ProductTranslations = new List<ProductTranslation>() {
        //        new ProductTranslation() {  
        //            Languages=await _context.Languages.ToListAsync(),

        //    }     

        //   };
        //    return new List<ProductEditViewModel>(d.ProductId); 
        //}
    }
}
