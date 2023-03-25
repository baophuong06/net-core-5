using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iShop.Web.ViewModel.Common
{
   public class PageList<T> : List<T>
    {
        public int PageIndex { private set; get; }
        public int PageSize { private set; get; }
        public int TotalPage { private set; get; }
        public int TotalCount { private set; get; }
        public bool HasPrevious {
            get {
                return (PageIndex > 1);
            }
        
        
        }
        public bool HasNext {
            get {
                return (PageIndex < TotalPage);
            }
        }
        
        public PageList(List<T> items,int count,int pageIndex,int pageSize)
        {
           
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public static  PageList<T> CreatePageList(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count =  source.Count();
            var items =  source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PageList<T>(items,count,  pageIndex, pageIndex);
        }
    }
}
