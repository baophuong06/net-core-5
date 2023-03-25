using iShop.Web.ViewModel.Catalog.Category;
using iShop.Web.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iShop.Application.Domain.Catalog.Category
{
  public  interface ICategoryService
    {
        Task<int> CreateCategory(CategoryCreateRequest request);
        Task<int> UpdateCategory(CategoryUpdateRequest request);
        Task<List<CategoryDisplayVM>> GetAllPage();
        Task<List<CategoryDisplayVM>> GetAll(string languageId);
        Task<CategoryViewModel> GetById( string languageId, int id);
        Task<List<CategoryDisplayVM>> GetAllCategory(string languageID);
    }
}
