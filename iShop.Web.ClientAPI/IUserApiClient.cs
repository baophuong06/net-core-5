using iShop.Web.ViewModel.Common;
using iShop.Web.ViewModel.System;
using iShop.Web.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.ClientAPI
{
  public  interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<List<UserViewModel>> GetUsers();//GetUserPagingRequest request);
        Task<PageList<UserViewModel>> GetPagingSearch(GetUserPagingRequest request);
        Task<ApiResult<bool>> RegisterUser(RegisterRequest request);
        Task<ApiResult<bool>> Update(Guid id, UpdateUserRequest request);
        Task<ApiResult<UserViewModel>> GetById(Guid id);
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
