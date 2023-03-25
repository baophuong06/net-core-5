using iShop.Web.ClientAPI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.ApplicationApp.Controllers.Components
{
    public class SiderBarViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;
        public SiderBarViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item =await _categoryApiClient.GetAllPage();
            return View(item);
        }
    }
}
