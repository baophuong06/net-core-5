using iShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace iShop.Web.ViewModel.Catalog.Category
{
   public class CategoryCreateRequest
    {
       // public int CategoryId { get; set; }
        public string Name { get; set; }
        public int ProductId{ get; set; }
       
        public int? ParentId { get; set; }
        public string SeoAlias { set; get; }
        public string LanguageId { get; set; }
        public List<SelectListItem> LanguageCategory { get; set; }
    }
}
