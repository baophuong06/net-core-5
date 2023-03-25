using iShop.Data.Entities;

using iShop.Web.ViewModel.Common;
using iShop.Web.ViewModel.System;
using iShop.Web.ViewModel.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iShop.Data.Entities.EF;
using iShop.Web.Unitity.Exceptions;

namespace iShop.Application.Domain.System.Users
{
    public class UserService : IUserService
    {
       // private readonly iShopDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            RoleManager<AppRole> roleManager, IConfiguration config)
        {
            //_context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            
            if (!result.Succeeded) {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
                new Claim(ClaimTypes.Name,request.UserName)
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
              _config["Tokens:Issuer"],
            claims,
              expires: DateTime.Now.AddHours(3),
              signingCredentials: credentials);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<PageList<UserViewModel>>GetPagingSearch(GetUserPagingRequest request)
        {
           
            if(request.keyword==null) {
                request.PageIndex = 1;
            }
            var page =await _userManager.Users.Where(x => x.UserName.Contains(request.keyword) || x.PhoneNumber.Contains(request.keyword))
                .OrderBy(x => x.Id)
               // .OrderByDescending(x=>x.UserName)
               // .Skip((request.PageIndex - 1) * request.PageSize)
                //.Take(request.PageSize)
                .Select(x=>new UserViewModel() {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    Dob=x.Dob,
                    Id = x.Id,
                    LastName = x.LastName
                })
                .ToListAsync();

            return PageList<UserViewModel>.CreatePageList(page,request.PageIndex,request.PageSize);
          
        }

        public async Task<ApiResult<UserViewModel>> GetUserById(Guid id)
        {
            var userId = await _userManager.FindByIdAsync(id.ToString());
            if (userId == null) {
                return new ApiErrorResult<UserViewModel>("khong tim thay");
            }
            var roles =await _userManager.GetRolesAsync(userId);
            var userVm = new UserViewModel() {
                Email=userId.Email,
                Id=userId.Id,
                PhoneNumber=userId.PhoneNumber,
                Dob=userId.Dob,
                FirstName=userId.FirstName,
                LastName=userId.LastName,
                UserName=userId.UserName,
                Roles=roles

            };
            return new ApiSuccessResult<UserViewModel>(userVm);
        }

        public  async Task<List<UserViewModel>> GetUsers()//GetUserPagingRequest request)
        {
            //request.PageIndex = request.PageIndex < 1 ? 1 : request.PageIndex;
            //request.PageSize = request.PageSize > 10 ? 10 : request.PageSize;
            //IQueryable<AppUser> user = from u in _userManager.Users
            //select u;
            //var query = _userManager.Users;
            //if(!string.IsNullOrEmpty(request.keyword)) {
            //     query =  query.Where(x => x.UserName.Contains(request.keyword) || x.PhoneNumber.Contains(request.keyword));
            //}
            //var totalRow = await query.CountAsync();
            var data = await _userManager.Users
            //    .Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new UserViewModel() {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FirstName = x.FirstName,

                    Id = x.Id,
                    LastName = x.LastName
                }).ToListAsync();
            ////Select and projecton
            //var pageResult = new PageResult<UserViewModel>() {
            //    TotalRecord = totalRow,
            //    Items = data
            //};
            return  new List<UserViewModel>(data);
           // return data;
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null) {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null) { return new ApiErrorResult<bool>("Email đã tồn tại"); }

             user = new AppUser() {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user,request.Password);
            if (result.Succeeded) {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user =await _userManager.FindByIdAsync(id.ToString());
            if(user==null) {
                return new ApiErrorResult<bool>("Tai khoan khong ton tai");
            }
            var removeRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            await _userManager.RemoveFromRolesAsync(user, removeRoles);
            var addRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach(var roleName in addRoles) {
                if(await _userManager.IsInRoleAsync(user,roleName)==false) {
                    await _userManager.AddToRolesAsync(user, addRoles);
                }

            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Update(Guid id, UpdateUserRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id)) {
                return new ApiErrorResult<bool>("Email da ton tai!");

            }
            var userId = await _userManager.FindByIdAsync(id.ToString());
            userId.Dob = request.Dob;
            userId.FirstName = request.FirstName;
            userId.LastName = request.LastName;
            userId.Email = request.Email;
            userId.PhoneNumber = request.PhoneNumber;
            var result = await _userManager.UpdateAsync(userId);
            if(result.Succeeded) {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cap nhat khong thanh cong");

        }
    }
}
