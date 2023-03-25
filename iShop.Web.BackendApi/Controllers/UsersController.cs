using iShop.Application.Domain.System.Users;
using iShop.Web.ViewModel.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.BackendApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromForm] LoginRequest request)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            var resultToken = await _userService.Authencate(request);
            if(string.IsNullOrEmpty(resultToken.ResultObj)) {
                return BadRequest(resultToken);//"UserName or Password correct!");
            }
           
            return Ok(new { Token = resultToken });
        }
        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(Guid id,[FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }//ModelState); }
            var result = await _userService.RoleAssign(id,request);
            if (!result.IsSuccessed) {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("register")]
        //[Consumes("multipart/form-data")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }//ModelState); }
            var result = await _userService.Register(request);
            if (!result.IsSuccessed) {
                return BadRequest("khong thanh cong");
            }
          
            return Ok();
        }
        //PUT: http://localhost/api/users/id
        [HttpPut("{id}")]
        
        public async Task<IActionResult> Update(Guid id,[FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid) 
                { return BadRequest(ModelState); }
            var result = await _userService.Update(id,request);
            if (!result.IsSuccessed) {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPaging()// GetUserPagingRequest request)
        {
            var userPaging = await _userService.GetUsers();
            return Ok(userPaging);
        }
        //http://localhost/api/users/search?pageIndex=1&pageSize=3&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetPageSearch([FromQuery]GetUserPagingRequest request)// GetUserPagingRequest request)
        {
            var userP = await _userService.GetPagingSearch(request);
            return Ok(userP);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(Guid id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }
    }
}
