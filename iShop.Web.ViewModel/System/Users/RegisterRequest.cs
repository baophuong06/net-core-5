using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iShop.Web.ViewModel.System.Users
{
   public class RegisterRequest
    {
       
        public string FirstName { get; set; }

       
        public string LastName { get; set; }

      
        public DateTime Dob { get; set; }

       
        public string Email { get; set; }

        
        public string PhoneNumber { get; set; }

        
        public string UserName { get; set; }

        
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
