using iShop.Web.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.Catalog.Products.Manager
{
  public  class CategoryList
    {
        public int Id { get; set; }
        public List<SelectItem> Categories { get; set; } = new List<SelectItem>();
    }
}
