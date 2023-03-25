using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace iShop.Data.Entities
{
    [Table("Carts")]
  public  class Cart
    {
        [Key]
        public int CartId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Guid UserId { get; set; }

        public Product Product { get; set; }

        public DateTime DateCreated { get; set; }

        public AppUser AppUser { get; set; }
    }
}
