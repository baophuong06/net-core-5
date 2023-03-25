using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.Catalog.Products.Manager
{
  public  class ProductCreateRequest
    {
        // public string Name { get; set; }
        // public decimal Price { get; set; }
        public decimal Price { set; get; }
                     // cột kiểu Money trong SQL Server (tương ứng decimal trong Model C#)
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        //  public int ViewCount { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public bool? IsFeatured { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public int CategoryId { get; set; }
        public IFormFile ThumnailImage { get; set; }
    }
}
