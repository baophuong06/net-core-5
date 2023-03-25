using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iShop.Data.Entities.enums;
namespace iShop.Data.Entities
{
    [Table("Categories")]
   public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public int SortOrder{ get; set; }
        public bool IsShowOnHome { get; set; }
        public int? ParentId { get; set; }
        public status Status { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }

        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
