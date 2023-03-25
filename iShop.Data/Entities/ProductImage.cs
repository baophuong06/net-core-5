using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using iShop.Data.Entities.enums;
namespace iShop.Data.Entities
{
    [Table("ProductImages")]
    public  class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }

        public int ProductId { get; set; }

        public string ImagePath { get; set; }

        public string Caption { get; set; }

        public bool IsDefault { get; set; }

        public DateTime DateCreated { get; set; }

        public int SortOrder { get; set; }

        public long FileSize { get; set; }

        public Product Products { get; set; }
    }
}
