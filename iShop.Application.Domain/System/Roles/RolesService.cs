using iShop.Data.Entities;
using iShop.Web.ViewModel.System.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using iShop.Web.ViewModel.Common;

namespace iShop.Application.Domain.System.Roles
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RolesService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel() {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
            return new ApiSuccessResult<List<RoleViewModel>> (roles);
        }
    }
}
