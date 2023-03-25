using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.Common
{
   public class PagingRequestBase 
    {
        //Ban muon tai du lieu o trang thu pageIndex=3 ,moi trang co 5sp tuc pagesize=5 phai bo qua
        //(pageindex-1)*pagesize=(3-1)*5=10 dong dau tien trang thu 3 dong 11
        //skip bo qua m dong dau tien
        //take lay dung pagesize dong du lieu
        //Tai khoan microsoft: tk1"tentk:nghean030@gmail.com matkhau:ngoc_1234;tk2:nguyenthingocna@gmail.com mat khau:ngoc@1234"
        public int PageSize { get; set; }//kich co cua mot trong vi du muon hien thi 5 san pham tren 1 page thi pagsize=5
        public int PageIndex { get; set; }//De lay du lieu o trang thu p=pageIndex,can bo qua (pageIndex-1)*pagesize
    }
}
