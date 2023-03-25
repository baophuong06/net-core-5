using iShop.Web.ViewModel.Unitities;
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
    public class SlideApiClient : ISlideApiClient
    {
        public readonly IConfiguration _configuration;
        public readonly IHttpClientFactory _httpClientFactory;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public SlideApiClient(IHttpClientFactory httpClientFactory,
                  IHttpContextAccessor httpContextAccessor,
                   IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<SlidesViewModel>> GetAll()
        {
            //return await GetListAsync<SlidesViewModel>("/api/slides");
            var client = _httpClientFactory.CreateClient();
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var reponse = await client.GetAsync($"/api/slides");
            var body = await reponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<SlidesViewModel>>(body);


        }
    }
}
