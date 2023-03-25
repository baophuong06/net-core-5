using iShop.Web.ClientAPI;
using iShop.Web.ViewModel.Common;
using iShop.Web.ViewModel.System;
using iShop.Web.ViewModel.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace iShop.Web.AdmidApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _user;
        private readonly IConfiguration _configuration;
        private readonly IRoleApiClient _role;
        public UserController(IUserApiClient user, IConfiguration configuration, IRoleApiClient role)
        {
            _user=user;
            _configuration = configuration;
            _role = role;
        }
        [HttpGet]
        public async Task<IActionResult> Index()//string Keyword, int pageIndex, int pageSize)
        {
            var session = HttpContext.Session.GetString("Token");
            //var request = new GetUserPagingRequest() {
                
            //    keyword = Keyword,
            //    PageIndex = pageIndex,
            //    PageSize = pageSize
            //};
            var data =await _user.GetUsers();
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()

        {
           // UpdateUserRequest use = new UpdateUserRequest();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)

        {
            if (!ModelState.IsValid)
                return View();
            var result = await _user.RegisterUser(request);
            if (result.IsSuccessed) {
                TempData["result"] = "Them moi thanh cong!";
                RedirectToAction("User","Index");
            }
          
            return View(request);
        }
       
        
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var user = await _user.GetById(id);
            if (user.IsSuccessed) {
                var result = user.ResultObj;
                var userUpdate = new UpdateUserRequest() {
                    Id = id,
                    Dob = result.Dob,
                    Email = result.Email,
                    // Id = result.Id,
                    LastName = result.LastName,
                    FirstName = result.FirstName,
                    PhoneNumber = result.PhoneNumber
                };
                return View(userUpdate);
            }

          return RedirectToAction("Error", "Index");

        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            if(!ModelState.IsValid)
                return View();
            
            var user = await _user.Update(request.Id, request);
            if(user.IsSuccessed) {
                 TempData["user"] = "Cap nhat thanh cong";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("Error", user.Message);
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _user.GetById(id);
            return View(result.ResultObj);
        }
        public async Task<IActionResult> Pages(string keyword, int pageIndex=1, int pageSize=5)
        {
            var requestPage = new GetUserPagingRequest() {
                keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var page = await _user.GetPagingSearch(requestPage);
            ViewBag.keyword = keyword;
            return View(page);
        }
        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var roleAssign = await GetRoleAssign(id);
            return View(roleAssign);

        }
        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _user.RoleAssign(request.Id, request);
            if (user.IsSuccessed) {
                TempData["user"] = ("Gan quyen thanh cong");
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("Error", user.Message);
            var roleAssig = await GetRoleAssign(request.Id);
            return View(roleAssig);
        }
        private async Task<RoleAssignRequest> GetRoleAssign(Guid id)
        {
            var userObject =await _user.GetById(id);
            var roleObject =await _role.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            foreach(var role in roleObject.ResultObj) {
                roleAssignRequest.Roles.Add(new SelectItem() {
                    Id=id.ToString(),
                    Name=role.Name,
                    Selected=userObject.ResultObj.Roles.Contains(role.Name)
                });
            }
            return roleAssignRequest;
        }
    }
}
