using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace iShop.Data.Entities
{
    [Table("ProductTranslations")]
   public class ProductTranslation
    {
        [Key]
        public int ProductTranslationId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }

        public Product Products { get; set; }

        public List<Language> Languages { get; set; }
    }
}
