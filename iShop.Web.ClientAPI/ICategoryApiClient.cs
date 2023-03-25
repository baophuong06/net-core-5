using iShop.Web.ViewModel.Catalog.Category;
using iShop.Web.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.ClientAPI
{
  public  interface ICategoryApiClient
    {
        Task<bool> CreateCategory(CategoryCreateRequest request);
        Task<List<CategoryDisplayVM>> GetAllPage();
        Task<CategoryViewModel> GetById(string languageId, int id);
        //Task<List<CategoryViewModel>> GetAllPage();
        Task<List<CategoryDisplayVM>> GetAll(string languageId);
        Task<List<CategoryDisplayVM>> GetAllCategory(string languageId);
    }
}
