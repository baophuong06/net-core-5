using iShop.Web.ViewModel.Common;
using iShop.Web.ViewModel.System.Roles;
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
    public class RoleApiClient : IRoleApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public RoleApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var reponse = await client.GetAsync($"/api/roles");
            var body = await reponse.Content.ReadAsStringAsync();
            if (reponse.IsSuccessStatusCode)
                // List<RoleViewModel> myDeserialize = (List<RoleViewModel>)JsonConvert.DeserializeObject(body, typeof(List<RoleViewModel>));
                //return new ApiSucessResult<List<RoleViewModle>>>(myDeserialize);
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<RoleViewModel>>>(body);
                //var options = new JsonSerializerOptions() {
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //};
                //var returnOrders = await JsonSerializer.DeserializeAsync<List<Order>>(await response.Content.ReadAsStreamAsync(), options);
            
              
            return JsonConvert.DeserializeObject<ApiErrorResult<List<RoleViewModel>>>(body);
        }
    }
}
