using iShop.Application.Domain.Unitities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
        public readonly ISlideService _slideService;
        public SlidesController(ISlideService slideService)
        {
            _slideService = slideService;
        }
        [HttpGet]
       
        public async Task<IActionResult> GetAll()
        {
            var slide = await _slideService.GetAll();
            return Ok(slide);
        }
    }
}
