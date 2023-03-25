using iShop.Application.Domain.System.Roles;
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
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _roleService;
        public RolesController(IRolesService rolesService)
        {
            _roleService = rolesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles =await _roleService.GetAll();
            return Ok(roles);
        }
    }
}
