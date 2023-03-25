using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iShop.Data.Entities.enums;
namespace iShop.Data.Entities
{
    [Table("Promotions")]
  public  class Promotion
    {
        [Key]
        public int PromotionId { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public bool ApplyForAll { set; get; }
        public int? DiscountPercent { set; get; }
        public decimal? DiscountAmount { set; get; }
        public string ProductIds { set; get; }
        public string ProductCategoryIds { set; get; }
        public status Status { set; get; }
        public string Name { set; get; }
    }
}
 