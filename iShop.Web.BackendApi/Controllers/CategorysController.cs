using iShop.Application.Domain.Catalog.Category;
using iShop.Data.Entities;
using iShop.Web.ViewModel.Catalog.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategorysController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CategoryCreateRequest request)
        {
            if(!ModelState.IsValid) {
                return BadRequest();
            }
           // var CategoryTranslationId = HttpContext.Request.Form["CategoryTranslationId"].ToString();
            
            var category =await _categoryService.CreateCategory(request);
            return Ok(category);
        }
        [HttpGet("languageId")]
        public async Task<IActionResult> GetAll(string LanguageId)
        {
            var category = await _categoryService.GetAll(LanguageId);
            return Ok(category);
        }
        [HttpGet("{id}/{languageId}")]
        public async Task<IActionResult> GetById( string languageId, int id)
        {
            var categoryById = await _categoryService.GetById(languageId, id);
            return Ok(categoryById);
        }
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAllCategory(string LanguageID)
        {
            var cate = await _categoryService.GetAllCategory(LanguageID);
            return Ok(cate);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPage()
        {
            var category = await _categoryService.GetAllPage();
            return Ok(category);
        }
    }
}
