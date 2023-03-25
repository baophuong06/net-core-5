using iShop.Web.ViewModel.Catalog.Products;
using iShop.Web.ViewModel.Catalog.Products.Manager;
using iShop.Web.ViewModel.Catalog.Products.Public;
using iShop.Web.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace iShop.Web.ClientAPI
{
   public interface IProductApiClient
    {
        Task<bool> CreateProduct(ProductCreateRequest product);
        Task<bool> UpdateProduct(ProductUpdateRequest request);
        Task<bool> Delete(int productId);
        Task<PageList<ProductViewModel>> GetAllPagingSearchProduct(GetProductPagingRequest request);
        Task<PageList<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
        Task<List<ProductViewModel>> GetAllCategoryById();
        // Task<IEnumerable<SelectListItem>> GetLanguage();
        // Task<IEnumerable<SelectListItem>> GetCategory(string languageID);
        Task<List<ProductViewModel>> GetFeatureProducts();
        Task<List<ProductViewModel>> GetFeatureProduct(string languageId, int take);
        Task<List<ProductViewModel>> GetLatedProducts(int take);
        Task<ProductViewModel> GetById(int productId, string languageId);
    }
}
