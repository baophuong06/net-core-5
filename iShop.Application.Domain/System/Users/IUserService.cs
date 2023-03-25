using iShop.Web.ViewModel.Common;
using iShop.Web.ViewModel.System;
using iShop.Web.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iShop.Application.Domain.System.Users
{
 public   interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<List<UserViewModel>> GetUsers();//GetUserPagingRequest request);
        Task<PageList<UserViewModel>> GetPagingSearch(GetUserPagingRequest request);
        Task<ApiResult<bool>> Update(Guid id, UpdateUserRequest request);
        Task<ApiResult<UserViewModel>> GetUserById(Guid id);
        Task<ApiResult<bool>> RoleAssign(Guid id,RoleAssignRequest request);
    }
}
