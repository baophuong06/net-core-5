using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iShop.Data.Entities.enums;

namespace iShop.Data.Entities
{
    [Table("Contacts")]
   public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Message { get; set; }
        public status Status { get; set; }
    }
}
