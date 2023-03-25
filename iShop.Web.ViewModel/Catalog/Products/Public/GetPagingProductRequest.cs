
using iShop.Web.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.Catalog.Products.Public
{
  public  class GetPagingProductRequest : PagingRequestBase
    {
      //  public string LanguageId { get; set; }
        public int? CategoryId { get; set; }
    }
    
}
