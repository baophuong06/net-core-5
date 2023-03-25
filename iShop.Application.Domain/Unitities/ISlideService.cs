using iShop.Web.ViewModel.Unitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iShop.Application.Domain.Unitities
{
  public  interface ISlideService
    {
        Task<List<SlidesViewModel>> GetAll();
    }
}
