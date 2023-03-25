using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.Common
{
  public  class PageResult<T>
    {
    public   List<T> Items { get; set; }
        public int TotalRecord { get; set; }
    }
}
