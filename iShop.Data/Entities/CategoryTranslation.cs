using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iShop.Data.Entities.enums;
namespace iShop.Data.Entities
{
    [Table("CategoryTranslations")]
    public class CategoryTranslation
    {
        [Key]
        public int CategoryTranslationId { set; get; }
        public int CategoryId { set; get; }
        public string Name { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string LanguageId { set; get; }
        public string SeoAlias { set; get; }

        public Categories Category { get; set; }

        public Language Language { get; set; }
    }
}
