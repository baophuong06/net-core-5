using iShop.Web.ViewModel.Catalog.Category;
using iShop.Web.ViewModel.Catalog.Products;
using iShop.Web.ViewModel.Catalog.Products.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.ApplicationApp.Models
{
    public class ProductDetailViewModel
    {
      public  CategoryViewModel CategoryVM { get; set; }
      public ProductViewModel ProductVM { get; set; }
        public List<ProductViewModel> RelatedProduct { get; set; }
        public List<ProductImageViewModel> ProductImage { get; set; }

    }
}
