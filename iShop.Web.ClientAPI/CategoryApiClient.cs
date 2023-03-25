using iShop.Web.ViewModel.Catalog.Category;
using iShop.Web.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace iShop.Web.ClientAPI
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        // private readonly IHttpClientFactory _httpClientFactory;
        // private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryApiClient(IHttpClientFactory httpClientFactory,
                  IHttpContextAccessor httpContextAccessor,
                   IConfiguration configuration)
           : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }
        public async Task<bool> CreateCategory(CategoryCreateRequest request)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new MultipartFormDataContent();
            httpContent.Add(new StringContent(request.ParentId.ToString()), "ParentId");
            httpContent.Add(new StringContent(request.ProductId.ToString()), "ProductId");
            httpContent.Add(new StringContent(request.LanguageId.ToString()), "LanguageId");
            httpContent.Add(new StringContent(request.Name.ToString()), "Name");
            httpContent.Add(new StringContent(request.SeoAlias.ToString()), "SeoAlias");
            var reponse = await client.PostAsync($"/api/categorys", httpContent);
            return reponse.IsSuccessStatusCode;
        }

        public async Task<List<CategoryDisplayVM>> GetAll(string languageId)
        {

            var data = await GetAsync<List<CategoryDisplayVM>>(
                   $"/api/categorys?languageId=" + languageId);

            return data;
        }

        public async Task<List<CategoryDisplayVM>> GetAllCategory(string languageId)
        {
            var data = await GetAsync<List<CategoryDisplayVM>>(
                    $"/api/categorys/{languageId}");

            return data;
        }

        public async Task<List<CategoryDisplayVM>> GetAllPage()
        {
            return await GetAsync<List<CategoryDisplayVM>>(
                $"/api/categorys");

        }

        public async Task<CategoryViewModel> GetById( string languageId, int id)
        {
            return await GetAsync<CategoryViewModel>($"/api/categorys/{id}/{languageId}");
        }
    }
}
