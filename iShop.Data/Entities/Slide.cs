using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iShop.Data.Entities.enums;
namespace iShop.Data.Entities
{
    [Table("Slides")]
  
   public class Slide
    {
            [Key]
        public int SlideId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Url { set; get; }

        public string Image { get; set; }
        public int SortOrder { get; set; }
        public status Status { set; get; }
    }
}
