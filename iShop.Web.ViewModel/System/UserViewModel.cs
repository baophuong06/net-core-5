using iShop.Web.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iShop.Web.ViewModel.System
{
   public class UserViewModel
    {
        public Guid Id { get; set; }

        [Display(Name ="Họ")]
        public string FirstName { get; set; }

        [Display(Name = "Tên")]
        public string LastName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime Dob { get; set; }

        public IList<string> Roles { get; set; }
    }
}
