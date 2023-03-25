
using iShop.Web.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.Catalog.Products.Manager
{
   public class GetProductPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }
       public string LangguageId { get; set; }
       public int? CategoryId { get; set; }
    }
}
