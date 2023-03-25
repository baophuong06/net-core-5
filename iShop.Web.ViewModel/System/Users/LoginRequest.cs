using iShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.System.Users
{
   public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        //public string LastName { get; set; }
        //public DateTime Dob { get; set; }
        //public List<Cart> Carts { get; set; }
        //public List<Order> Orders { get; set; }
        //public List<Transaction> Transactions { get; set; }
    }
}
