using iShop.Web.ClientAPI;
using iShop.Web.Unitity.Constant;
using iShop.Web.ViewModel.Catalog.Products;
using iShop.Web.ViewModel.Catalog.Products.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.AdmidApp.Controllers
{
 
    public class ProductController : Controller
    {
        private readonly IConfiguration _configutation;
        private readonly IProductApiClient _produc;

        public ProductController(IConfiguration configuration,IProductApiClient product)
        {
            _produc=product;
            _configutation = configuration;
        }
       
        public async Task<IActionResult> GetCategory()
        {
            var cate =await _produc.GetAllCategoryById();
            
            return View(cate);
        }
        public async Task<IActionResult> Index(string keyword,int pageSize=10,int pageIndex=1)
        {
            var request = new GetProductPagingRequest() {
                keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var data = await _produc.GetAllPagingSearchProduct(request);
            ViewBag.keyword = keyword;
           
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            if(!ModelState.IsValid) {
                return View(request);
            }
            var product =await _produc.CreateProduct(request);
            if (product) {
                TempData["product"] = "Create product successfully!";
                return RedirectToAction("Index");
            }
            TempData["product"] = "Create product unsuccessfully";
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var product = await _produc.GetById(id, languageId);
            var editVM = new ProductUpdateRequest() {
                Id = product.ProductId,
                Description = product.Description,
                Details = product.Details,
                Name = product.Name,
                SeoAlias = product.SeoAlias,
                SeoDescription = product.SeoDescription,
                SeoTitle = product.SeoTitle,
                LanguageId = product.LanguageId,
               // ThumnailImage = product.ThumbnailImage
            };

            return View(editVM);
        }
       [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if(!ModelState.IsValid) {
                return BadRequest();
            }
            var pr = await _produc.UpdateProduct(request);
            if(pr) {
                TempData["pr"] = "Update product successfully!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("","Update product unsuccessfully!");
            return View(request);
        }
        [HttpGet]
        public IActionResult Delete(int productId)
        {
            return View(new ProductDeleteRequest() { ProductID=productId});
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProductDeleteRequest request)
        {
            if(!ModelState.IsValid) {
                return View();
            }
            var result =await _produc.Delete(request.ProductID);
            if (result) {
                TempData["result"] = "Xóa sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }


    }
}
