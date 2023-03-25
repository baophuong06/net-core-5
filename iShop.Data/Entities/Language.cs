using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace iShop.Data.Entities
{
    [Table("Languages")]
  public  class Language
    {
        [Key]
        public string LanguageId { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public List<ProductTranslation> ProductTranslations { get; set; }

        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
