using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace iShop.Data.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]                              // Cột trong DB, Not Null
        [StringLength(50)]                      // nvarchar(50)
        public string Name { set; get; }

        [Column(TypeName = "Money")]              // cột kiểu Money trong SQL Server (tương ứng decimal trong Model C#)
        public decimal Price { set; get; }
        [Column(TypeName = "Money")]              // cột kiểu Money trong SQL Server (tương ứng decimal trong Model C#)
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreate { set; get; }
        [StringLength(100)]
        public string SeoAlias { set; get; }
        public bool? IsFeatured { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public List<Cart> Carts { get; set; }

        public List<ProductTranslation> ProductTranslations { get; set; }

        public List<ProductImage> ProductImages { get; set; }
    }
}
