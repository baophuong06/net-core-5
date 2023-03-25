using iShop.Web.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.Catalog.Category
{
   public class GetPagingCategoryRequest : PagingRequestBase 
    {
        public string Keyword { get; set; }
        public List<int> categoryIds { get; set; }
    }
}
