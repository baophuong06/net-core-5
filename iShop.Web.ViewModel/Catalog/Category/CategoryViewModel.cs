using iShop.Data.Entities;
using iShop.Data.Entities.enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace iShop.Web.ViewModel.Catalog.Category
{
   public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public bool IsShowOnHome { get; set; }
        public int? ParentId { get; set; }
        public status Status { get; set; }
        public int ProductId { get; set; }
        public List<CategoryTranslation> CategoryTransions { get; set; } //= new List<SelectListItem>();
        public string LanguageId { get; set; }
    }
}
