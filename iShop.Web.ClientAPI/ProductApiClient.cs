using iShop.Web.Unitity.Constant;
using iShop.Web.ViewModel.Catalog.Products;
using iShop.Web.ViewModel.Catalog.Products.Manager;
using iShop.Web.ViewModel.Catalog.Products.Public;
using iShop.Web.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace iShop.Web.ClientAPI
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        public ProductApiClient(IHttpClientFactory httpClientFactory,
                 IHttpContextAccessor httpContextAccessor,
                  IConfiguration configuration)
          : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }
        public async Task<bool> CreateProduct(ProductCreateRequest product)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var json = JsonConvert.SerializeObject(product);
            var httpContent = new MultipartFormDataContent();
            if(product.ThumnailImage!=null) {
                byte[] data;
                using (var br = new BinaryReader(product.ThumnailImage.OpenReadStream())) {
                    data = br.ReadBytes((int)product.ThumnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                httpContent.Add(bytes, "thumnailImage", product.ThumnailImage.FileName);

            }
            httpContent.Add(new StringContent(product.Price.ToString()), "Price");
            httpContent.Add(new StringContent(product.OriginalPrice.ToString()), "OriginalPrice");
            httpContent.Add(new StringContent(product.Stock.ToString()), "Stock");
            httpContent.Add(new StringContent(product.Name.ToString()), "Name");
            httpContent.Add(new StringContent(product.Description.ToString()), "Description");
            httpContent.Add(new StringContent(product.SeoAlias.ToString()), "SeoAlias");
            httpContent.Add(new StringContent(product.Details.ToString()), "Details");
            httpContent.Add(new StringContent(product.SeoDescription.ToString()), "SeoDescription");
            httpContent.Add(new StringContent(product.SeoTitle.ToString()), "SeoTitle");
            httpContent.Add(new StringContent(product.LanguageId.ToString()), "LanguageId");
            var response = await client.PostAsync($"/api/products/", httpContent);
            return response.IsSuccessStatusCode;
            //var result = await response.Content.ReadAsStringAsync();
            //if (response.IsSuccessStatusCode)
            //{
            //    return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            //}
                
            //return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);

        }
        

        public async Task<PageList<ProductViewModel>> GetAllPagingSearchProduct(GetProductPagingRequest request)
        {
            //var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            //var client = _httpClientFactory.CreateClient();
            //client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var data = await GetAsync<PageList<ProductViewModel >> (
               $"/api/products/paging?pageIndex={request.PageIndex}" +
               $"&pageSize={request.PageSize}" +
               $"&keyword={request.keyword}");

            return data;
            //PageList<ProductViewModel> myDeserialize = (PageList<ProductViewModel>)JsonConvert.DeserializeObject(body, typeof(PageList<ProductViewModel>));
            //return new PageList<ProductViewModel>(myDeserialize);
        }

           // return JsonConvert.DeserializeObject<ApiErrorResult<PageList<ProductViewModel>>>(body);

            // return JsonConvert.DeserializeObject<PageList<ProductViewModel>>(body);
            //PageList<ProductViewModel> myDeserialize = (PageList<ProductViewModel>)
            //     JsonConvert.DeserializeObject(body, typeof(PageList<ProductViewModel>));
            //return myDeserialize;

        

        public async Task<bool> UpdateProduct(ProductUpdateRequest request)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new MultipartFormDataContent();
            if(request.ThumnailImage!=null) {
                byte[] data;
                using (var br = new BinaryReader(request.ThumnailImage.OpenReadStream())) {
                    data = br.ReadBytes((int)request.ThumnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                httpContent.Add(bytes, "thumnailImage", request.ThumnailImage.FileName);
              
            }

            httpContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
            httpContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");
            httpContent.Add(new StringContent(string.IsNullOrEmpty(request.Details) ? "" : request.Details.ToString()), "detail");
            httpContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoDescription) ? "" : request.SeoDescription.ToString()), "seoDescription");
            httpContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoTitle) ? "" : request.SeoTitle.ToString()), "seoTitle");
            httpContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoAlias) ? "" : request.SeoAlias.ToString()), "seoAlias");
            httpContent.Add(new StringContent(languageId), "languageId");
            var reponse = await client.PostAsync($"/api/products" + request.Id, httpContent);
            return reponse.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int productId)
        {
            return await Delete($"/api/products/"+ productId);

            //var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            //// var json = JsonConvert.SerializeObject(productId);
            //var client = _httpClientFactory.CreateClient();
            //client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            //// var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            //var reponse = await client.DeleteAsync($"/api/products/"+productId);
            // return reponse.IsSuccessStatusCode;
             //    return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);
             //return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }

        public async Task<List<ProductViewModel>> GetAllCategoryById()
        {

            var data = await GetAsync<List<ProductViewModel>>(
              $"/api/products/categoryId");

            return data;
            //var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            //var client = _httpClientFactory.CreateClient();
            //client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            //var reponse = await client.GetAsync($"/api/products/categoryId");
            //var body = await reponse.Content.ReadAsStringAsync();

            //     return JsonConvert.DeserializeObject<List<ProductViewModel>>(body);
            // PageList<ProductViewModel> myDeserialize = (PageList<ProductViewModel>)JsonConvert
            //.DeserializeObject(body, typeof(PageList<ProductViewModel>));
            // return new ApiSuccessResult<PageList<ProductViewModel>>(myDeserialize);

        }

        public async Task<List<ProductViewModel>> GetFeatureProducts()
        {
            var data= await GetAsync<List<ProductViewModel>>($"/api/products/featured");
            return data;
        }
      public async  Task<List<ProductViewModel>> GetFeatureProduct(string languageId, int take)

        {
            var data = await GetAsync<List<ProductViewModel>>($"/api/products/featured/{languageId}/{take}");
            return data;
        }
        public async Task<List<ProductViewModel>> GetLatedProducts(int take)
        {
            var data = await GetAsync<List<ProductViewModel>>($"/api/products/lated/"+ take);
            return data;
        }

        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var data = await GetAsync<ProductViewModel>($"/api/products/{productId}/{languageId}");
            return data;
        }

        public async Task<PageList<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            var data = await GetAsync<PageList<ProductViewModel>>(
              $"/api/products/allpaging?pageIndex={request.PageIndex}" +
              $"&pageSize={request.PageSize}" +
              $"&keyword={request.keyword}&languageId={request.LangguageId}&categoryId={request.CategoryId}");
            return data;
        }


        //public Task<IEnumerable<SelectListItem>> GetLanguage()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<SelectListItem>> GetCategory(string languageID)
        //{
        //    throw new NotImplementedException();
        //} 
    }
}
