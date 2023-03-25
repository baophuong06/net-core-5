using iShop.Web.ViewModel.Common;
using iShop.Web.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iShop.Application.Domain.System.Roles
{
   public interface IRolesService
    {
       Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}
