using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace iShop.Web.ViewModel.Catalog.Products
{
   public class ProductEditViewModel
    {
        public int ProductId { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreate { set; get; }

     
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        [Required]
        [Display(Name = "Language")]
        public string SelectedLanguageId { get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
        public bool? IsFeatured { get; set; }
        [Required]
        [Display(Name = "CategoryTranslation")]
        public int SelectedCategoryId { get; set; }
        public IEnumerable<SelectListItem> CategoryTranslations { get; set; }
        public string ThumbnailImage { get; set; }

        
    }
}
