
using iShop.Web.ViewModel.Catalog.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.ClientAPI;
//using System.Web.Mvc.Re;

namespace iShop.Web.AdmidApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IConfiguration _configutation;
        public CategoryController(ICategoryApiClient categoryApiClient, IConfiguration configuration)
        {
            _configutation = configuration;
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IActionResult> Index(string languageId)
        {
           
          
            var cate = await _categoryApiClient.GetAllPage();
            

            var acteL = await _categoryApiClient.GetAll(languageId);
            ViewBag.Categories = acteL.Select(x => new SelectListItem() {
                Text = x.CategoryName,
                Value = x.SelectedLanguageId.ToString()
            });
            return View(cate);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]CategoryCreateRequest category,string languageId)
        {
           if(!ModelState.IsValid) { return View(); }
            var cate =await _categoryApiClient.CreateCategory(category);

            var acteL = await _categoryApiClient.GetAll(languageId);
            ViewBag.Categories = acteL.Select(x => new SelectListItem() {
                Text = x.CategoryName,
                Value = x.SelectedLanguageId.ToString()
            });
            if (cate) {
                TempData["cate"] = "Them san pham thanh cong";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Them san pham that bai");
            return View(category);
        }
    }
}
