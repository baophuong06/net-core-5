using iShop.Data.Entities.EF;
using iShop.Web.ViewModel.Unitities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iShop.Application.Domain.Unitities
{
    public class SlideService : ISlideService
    {
        private readonly iShopDbContext _context;
        public SlideService(iShopDbContext context)
        {
            _context = context;
        }
        public async Task<List<SlidesViewModel>> GetAll()
        {
            var slide =await _context.Slides
                .OrderBy(x => x.SortOrder)
                .Select(x => new SlidesViewModel() {
                    ID = x.SlideId,
                    Name = x.Name,
                    Description = x.Description,
                    Url = x.Url,
                    Image = x.Image,
                   
                }).ToListAsync();
            return slide;
        }
    }
}
