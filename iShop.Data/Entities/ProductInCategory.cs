using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iShop.Data.Entities.enums;
namespace iShop.Data.Entities
{
    [Table("ProductInCategories")]

    public class ProductInCategory
    {
        [Key]
        public int ProductInCategoryId { get; set; }

        public Product Products { get; set; }

        public int CategoryInProductId { get; set; }

        public Categories Category { get; set; }
    }
}
