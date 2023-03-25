using iShop.Web.ViewModel.Catalog.Category;
using iShop.Web.ViewModel.Catalog.Products;
using iShop.Web.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.ApplicationApp.Models
{
    public class ProductCategoryViewModel
    {
        public CategoryViewModel Categorys { get; set; }
        public PageList<ProductViewModel> Products { get; set; }
    }
}
