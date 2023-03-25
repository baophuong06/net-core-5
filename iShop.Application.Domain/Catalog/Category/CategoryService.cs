using iShop.Data.Entities;
using iShop.Data.Entities.EF;
using iShop.Web.ViewModel.Catalog.Category;
using iShop.Web.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using iShop.Web.ViewModel.System.Language;

namespace iShop.Application.Domain.Catalog.Category
{
    
    public class CategoryService : ICategoryService
    {
        private readonly iShopDbContext _context;
        public CategoryService(iShopDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateCategory(CategoryCreateRequest request)
        {
            //var lisTra = (from pt in _context.CategoryTranslations
                         
            //              select pt
            //        ).ToList();

            Categories category = new Categories();
            category.ParentId = request.ParentId;
            // category.CategoryTranslations=request.LanguageId
            request.LanguageCategory = _context.CategoryTranslations.Select(x => new SelectListItem() {
                Value = x.LanguageId.ToString(),
                Text = x.Name
            }).ToList();
            category.CategoryTranslations = new List<CategoryTranslation>() {
              new  CategoryTranslation(){
                  Name=request.Name,
                  SeoAlias=request.SeoAlias,
                 LanguageId=request.LanguageId
                }
            };
            
            //var lisTra = (from pt in _context.CategoryTranslations
            //              where pt.LanguageId==request.LanguageId
            //              select pt
            //        ).ToList();
            //Categories category = new Categories();
            //category.ParentId = request.ParentId;
            //lisTra.Insert(0,new CategoryTranslation { CategoryTranslationId=0, Name = "Select" });

            //category.CategoryTranslations = new List<CategoryTranslation>() {
            //    new CategoryTranslation() {
            //        LanguageId=request.LanguageId,
            //        Name=request.Name,
            //        SeoAlias=request.SeoAlias
            //    }

            //};
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category.CategoryId;
        }

        public async Task<List<CategoryDisplayVM>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.CategoryId equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };
            return await query.Select(x => new CategoryDisplayVM() {
               
                CategoryName = x.ct.Name

            }).ToListAsync();
        }

        public async Task<List<CategoryDisplayVM>> GetAllPage()
        {
            //select jion
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.CategoryId equals ct.CategoryId
                        //where ct.LanguageId == languageId
                        select new { c, ct };
            return await query.Select(x => new CategoryDisplayVM() {
               CategoryID=x.c.CategoryId,
                CategoryName = x.ct.Name,
                ParentId=x.c.ParentId,
               SelectedLanguageId =x.ct.LanguageId,
               
            }).ToListAsync();
        }

        public async Task<CategoryViewModel> GetById( string languageId, int id)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.CategoryId equals ct.CategoryId
                        where ct.LanguageId == languageId && c.CategoryId == id
                        select new { c, ct };
            return await query.Select(x => new CategoryViewModel() {
                CategoryId = x.c.CategoryId,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).FirstOrDefaultAsync();
        }

        public async Task<List<CategoryDisplayVM>> GetAllCategory(string languageID)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.CategoryId equals ct.CategoryId
                        where ct.LanguageId==languageID
                        select new {  c, ct };
            return await query.Select(a => new CategoryDisplayVM() {
                CategoryID = a.c.CategoryId,
                CategoryName = a.ct.Name,
                SelectedLanguageId = a.ct.LanguageId,
                /////Languages = GetCategory(a.ct.LanguageId),
            }).ToListAsync();
            //var product = await query//.Include(a => a.pt.Languages).Include(a => a.pic.Category)
            //    .Select(a =>
            //     new ProductViewModel() {
            //         Name = a.p.Name,
            //         Price = a.p.Price,
            //         LanguageId = a.pt.LanguageId,
            //         CategoryName = a.ct.Name
            //     }
            //    ).ToListAsync();

         
        }
        public  IEnumerable<SelectListItem> GetCategory(string languageID)
        {
            if (!String.IsNullOrWhiteSpace(languageID)) {
                var query = _context.CategoryTranslations.AsNoTracking()
                              .OrderBy(x => x.Name)
                              .Where(x => x.LanguageId == languageID)
                              .Select(a => new SelectListItem {
                                  Value = a.CategoryId.ToString(),
                                  Text = a.Name
                              }).ToList();

                return new SelectList(query, "Value", "Text");
            }
            return null;
        }
        public Task<int> UpdateCategory(CategoryUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
