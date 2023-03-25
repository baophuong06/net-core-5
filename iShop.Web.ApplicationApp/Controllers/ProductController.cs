using iShop.Web.ApplicationApp.Models;
using iShop.Web.ClientAPI;
using iShop.Web.ViewModel.Catalog.Products.Manager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.ApplicationApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient,ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IActionResult> Category(string culture,int id, int page=1)
        {
            var categories =await _productApiClient.GetAllPaging(new GetProductPagingRequest() {
                CategoryId = id,
                PageIndex = page,
                LangguageId = culture,
                PageSize = 10
            });
            return View(new ProductCategoryViewModel() { 
                Categorys=await _categoryApiClient.GetById(culture,id),
                Products=categories
            });
        }

        public async Task<IActionResult> Details(int productId,string culture)
        {
            var productDetail = await _productApiClient.GetById(productId,culture);
            return View(new ProductDetailViewModel() { 
                ProductVM=productDetail
            });
        }
    }
}
