using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iShop.Data.Entities
{
    [Table("OrderDetails")]
   public class OrderDetail
    {
        [Key]
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Order Orders { get; set; }

        public Product Products { get; set; }
    }
}
