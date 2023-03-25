using iShop.Web.ViewModel.Catalog.Products;
using iShop.Web.ViewModel.Unitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.ApplicationApp.Models
{
    public class HomeViewModel
    {
        public List<SlidesViewModel> Slides { get; set; }
        public List<ProductViewModel> FeaturedProducts { get; set; }
        public List<ProductViewModel> LatedProducts { get; set; }
    }
}
