using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace iShop.Web.ViewModel.Catalog.Category
{
  public  class CategoryDisplayVM
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }
        // public string CateName { get; set; }
        [Required]
        [Display(Name = "Language")]
        public string SelectedLanguageId { get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
       // public string CategoryName { get; set; }

    }
}
