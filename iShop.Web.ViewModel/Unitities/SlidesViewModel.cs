using iShop.Data.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iShop.Web.ViewModel.Unitities
{
   public class SlidesViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set;}
        public string Url { set; get; }

        public string Image { get; set; }
        public int SortOrder { get; set; }
        public status Status { set; get; }
    }
}
