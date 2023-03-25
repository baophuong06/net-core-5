using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.Catalog.Products.Manager
{
  public  class ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public bool? IsFeatured { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public IFormFile ThumnailImage { get; set; }
    }
}
