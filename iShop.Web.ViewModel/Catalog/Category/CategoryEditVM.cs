using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace iShop.Web.ViewModel.Catalog.Category
{
   public class CategoryEditVM
    {
     
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Language")]
        public string SelectedLanguageId { get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
        
        [Required]
        [Display(Name = "CategoryTranslation")]
        public int SelectedCategoryId { get; set; }
        public IEnumerable<SelectListItem> CategoryTranslations { get; set; }
        public string ThumbnailImage { get; set; }

    }
}
