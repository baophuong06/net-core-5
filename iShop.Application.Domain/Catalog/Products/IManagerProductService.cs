
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using iShop.Web.ViewModel.Common;
using iShop.Web.ViewModel.Catalog.Products;

using Microsoft.AspNetCore.Http;
using iShop.Web.ViewModel.Catalog.Products.Public;
using iShop.Web.ViewModel.Catalog.Products.Manager;
using System.Web.Mvc;

namespace iShop.Application.Domain.Catalog.Products
{
   public interface IManagerProductService
    {
        Task<int> Create(ProductCreateRequest request);
       // Task<int> CreateCategory(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int ProductId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task AddViewCount(int productId);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
        Task<List<ProductImageViewModel>> GetListImage(int productId);
        Task<ProductViewModel> GetById(int productId, string languageId);
        Task<ProductImageViewModel> GetImageById(int imageId);
        Task<PageResult<ProductViewModel>> GetAllCategoryId(GetPagingProductRequest request, string languageId);
        Task<List<ProductViewModel>> GetAllCategoryById();//CategoryList category, int id);
        Task<PageList<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
        Task<PageList<ProductViewModel>> GetAllPagingSearchProduct(GetProductPagingRequest request);
        Task<List<ProductViewModel>> GetFeatureProducts();//int take la so luong sp muon lay
        Task<List<ProductViewModel>> GetLatedProducts(int take);
        Task<IEnumerable<SelectListItem>> GetLanguage();
        Task<IEnumerable<SelectListItem>> GetCategory(string languageID);
        Task<int> GetAmount();
        // Task<List<ProductEditViewModel>> CreateCategory();

        Task<List<ProductViewModel>> GetFeatureProduct(string languageId, int take);//int take la so luong sp muon lay
        Task<List<ProductViewModel>> GetLatedProduct(int take,string languageId);
    }
}
