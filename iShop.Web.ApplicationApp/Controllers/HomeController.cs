using iShop.Web.ClientAPI;
using iShop.Web.ApplicationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static iShop.Web.Unitity.Constant.SystemConstants;
using iShop.Web.ViewModel.Catalog.Products.Public;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using LazZiya.ExpressLocalization;

namespace iShop.Web.ApplicationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISlideApiClient _slideApiClient;
        private readonly IProductApiClient _productApiClient;
        private readonly ISharedCultureLocalizer _loc;
        public HomeController(ILogger<HomeController> logger,ISlideApiClient slideApiClient,IProductApiClient productApiClient)//,ISharedCultureLocalizer loc)
        {
            _logger = logger;
            _slideApiClient = slideApiClient;
            _productApiClient = productApiClient;
            //_loc = loc;
        }

        public async Task<IActionResult> Index(int take=6)
        {
           // var msg = _loc.GetLocalizedString("Vietnamese");

            var slideVM = new HomeViewModel() {
                Slides = await _slideApiClient.GetAll(),
                FeaturedProducts = await _productApiClient.GetFeatureProducts(),
                LatedProducts=await _productApiClient.GetLatedProducts(take)
                //ProductSettings.NumberOfFeaturedProducts)
            };
            return View(slideVM);
        }

        public IActionResult Privacy()
        {
           
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SetCultureCookie(string cltr,string returnUrl)
        {
            Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
               );

            return LocalRedirect(returnUrl);
        }
    }
}
