using iShop.Web.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.System.Users
{
   public class GetUserPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }
    }
}
