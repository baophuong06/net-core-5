using iShop.Web.ViewModel.Common;
using iShop.Web.ViewModel.System;
using iShop.Web.ViewModel.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace iShop.Web.ClientAPI
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
       // private readonly IHttpContextAccessor _httpContextAccessor;
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
           _httpContextAccessor = httpContextAccessor;
            
        }
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("/api/users/authenticate", httpContent);
            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync());
            // var token = await response.Content.ReadAsStringAsync();
            //return token;
        }
        ///paging?pageIndex=" + $"{ request.PageIndex}&pageSize={request.PageSize}&keword={request.keyword}
        public async Task<List<UserViewModel>> GetUsers()//GetUserPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var reponse = await client.GetAsync($"/api/users");
            var body = await reponse.Content.ReadAsStringAsync();
          
               var users= JsonConvert.DeserializeObject<List<UserViewModel>>(body);

            return users;
        }
        //
        public async Task<ApiResult<bool>> RegisterUser(RegisterRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            // RegisterRequest registerRequest=new RegisterRequest ();
           
            
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var reponse = await client.PostAsync($"/api/users", httpContent);
            var result = await reponse.Content.ReadAsStringAsync();
            if (reponse.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<bool>> Update(Guid id, UpdateUserRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(request);
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",session);
           
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var reponse = await client.PutAsync($"/api/users/{id}", httpContent);
            if (reponse.IsSuccessStatusCode) 
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(await reponse.Content.ReadAsStringAsync());
            
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(await reponse.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

            var response = await client.GetAsync($"/api/users/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<ApiSuccessResult<UserViewModel>>(body);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<UserViewModel>>(body);
        }

        public async Task<PageList<UserViewModel>> GetPagingSearch(GetUserPagingRequest request)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var response = await client.GetAsync($"/api/users/paging?pageIndex=" +
               $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.keyword}");
            var body =await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PageList<UserViewModel>>(body);
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(request);
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var reponse = await client.PutAsync($"/api/users/{id}/roles", httpContent);

            if (reponse.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(await reponse.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(await reponse.Content.ReadAsStringAsync());
        }
    }
}
