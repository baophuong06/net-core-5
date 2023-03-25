using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.ViewModel.Catalog.Products.Public;
using iShop.Application.Domain.Catalog.Products;
using iShop.Web.ViewModel.Catalog.Products.Manager;
using Microsoft.AspNetCore.Authorization;

namespace iShop.Web.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
       
        private readonly IManagerProductService _managerProductServive;
        public ProductsController(IManagerProductService managerProductServive)
        {
            
            _managerProductServive = managerProductServive;
        }
        //http://localhost:post/product
        [HttpGet]
        //public async Task<ActionResult> Get()
        //{
        //    var product = await _managerProductServive.GetAll();
        //    return Ok(product);
        //}
        //http://localhost:post/product?pageIndex=1&pageSize=10&CategoryId
        [HttpGet("{languageId}")]
        public async Task<ActionResult> GetPaging(string languageId,[FromQuery]GetPagingProductRequest request)
        {
            var product = await _managerProductServive.GetAllCategoryId(request, languageId);
            return Ok(product);
        }
        //http://localhost:post/product/1
        [HttpGet("{productId}/{languageId}")]
        public async Task<ActionResult> GetById(int productId, string languageId)
        {
            var product = await _managerProductServive.GetById(productId, languageId);
            if (product == null)
                return BadRequest("Cannot find product :");
            return Ok(product);

        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<ActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if(!ModelState.IsValid) {
                return BadRequest();
            }
            var productId = await _managerProductServive.Create(request);
            if (productId == 0)
                return BadRequest();
            var product = await _managerProductServive.GetById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { productId }, product);
        }

        [HttpPut("{productId}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<ActionResult> Update([FromRoute] int procuctId,ProductUpdateRequest request)
        {
            if(!ModelState.IsValid) {
                return BadRequest();
            }
            request.Id = procuctId;
            var effetedResult = await _managerProductServive.Update(request);
            if (effetedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("productId")]
        public async Task<ActionResult> Delete(int productId)
        {
            var effetedResult = await _managerProductServive.Delete(productId);
            if (effetedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpPut("{price}/{productId}/{newPrice}")]
        public async Task<ActionResult> UpdatePrice([FromQuery]int productId, decimal newPrice)
        {
            var isSuccessful = await _managerProductServive.UpdatePrice(productId, newPrice);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }

        [HttpPost("{productId}/{images}")]
        public async Task<ActionResult> CreateImage(int productId, [FromForm]ProductImageCreateRequest request )
        {
           if(!ModelState.IsValid) {
                return BadRequest();
            }
            var imageId = await _managerProductServive.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();
            var image = await _managerProductServive.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById), new {id= imageId }, image);
        }

        [HttpPut("{productId}/{images}/{imageId}")]
        public async Task<ActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            var image = await _managerProductServive.UpdateImage(imageId, request);
            if (image == 0)
                return BadRequest();
           
            return Ok();
        }

        [HttpDelete("{productId}/{images}/imageId")]
        public async Task<ActionResult> DeleteImage(int imageId)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            var image = await _managerProductServive.RemoveImage(imageId);
            if (image== 0)
                return BadRequest();

            return Ok();
        }
        [HttpGet("{productId}/{images}/{ImageId}")]
        public async Task<ActionResult> GetImageById(int imageId)
        {
            var image = await _managerProductServive.GetImageById(imageId);
            if (image==null)
               
            return BadRequest("Cannot find image");
            return Ok();
        }
        //http://localhost/api/products/paging?pageIndex=1&pageSize=2&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetPagingProduct([FromQuery]GetProductPagingRequest request)// GetUserPagingRequest request)
        {
            var product = await _managerProductServive.GetAllPagingSearchProduct(request);
            return Ok(product);
        }
        //http://localhost/api/products/paging?pageIndex=1&pageSize=2&keyword=
        [HttpGet("allpaging")]
        public async Task<IActionResult> GetAllPgings([FromQuery] GetProductPagingRequest request)// GetUserPagingRequest request)
        {
            var product = await _managerProductServive.GetAllPaging(request);
            return Ok(product);
        }
        [HttpGet("products/categoryId")]
        public async Task<IActionResult> GetCategoryById(string languageId)
        {
            var category =await _managerProductServive.GetAllCategoryById();
            // category = _managerProductServive.GetLanguage();
            var getC =await _managerProductServive.GetLanguage();
            getC =await _managerProductServive.GetCategory(languageId);
            return Ok(category);
        }

        [HttpGet("featured")]
        //http://localhost/api/products/featured
        public async Task<IActionResult> GetFeatureProducts()
        {
            var featureP = await _managerProductServive.GetFeatureProducts();
            // category = _managerProductServive.GetLanguage();
         
            return Ok(featureP);
        }
        [HttpGet("featured/{languageId}/{take}")]
        //http://localhost/api/products/featured
        public async Task<IActionResult> GetFeatureProduct(string languageId, int take)
        {
            var featureP = await _managerProductServive.GetFeatureProduct(languageId,take);
            // category = _managerProductServive.GetLanguage();

            return Ok(featureP);
        }

        [HttpGet("lated/{take}")]
        //http://localhost/api/products/lated/take=take.Take
        public async Task<IActionResult> GetLatedProducts(int take)
        {
            var featureL = await _managerProductServive.GetLatedProducts(take);
            // category = _managerProductServive.GetLanguage();

            return Ok(featureL);
        }
    }
}
