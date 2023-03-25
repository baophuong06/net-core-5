using iShop.Web.ViewModel.Unitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.ClientAPI
{
   public interface ISlideApiClient
    {
        Task<List<SlidesViewModel>> GetAll();
    }
}
