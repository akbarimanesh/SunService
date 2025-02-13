using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Repos.Ef.SunServices.HService
{
    public class HomeServiceRepository : IHomeServiceRepository
    {
        private readonly AppDbContext _appDbContext;

        public HomeServiceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateHomeService(HomeService homeService, CancellationToken cancellationToken)
        {
            await _appDbContext.HomeServices.AddAsync(homeService, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteHomeService(int id, CancellationToken cancellationToken)
        {
            var homeService = await _appDbContext.HomeServices.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.HomeServices.Remove(homeService);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<HomeServiceDto>> GetAllHomeService(CancellationToken cancellationToken)
        {
            return await _appDbContext.HomeServices.AsNoTracking().Select(x => new HomeServiceDto()
            {
                
                Title = x.Title,
                Description = x.Description,
                BasePrice = x.BasePrice,
                ImagePath = x.ImagePath,
                NumberVisits = x.NumberVisits,
                SubCategoryTitle=x.SubCategory.Title
             

            }).ToListAsync(cancellationToken);
        }

        public async Task<HomeService> GetHomeServiceById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.HomeServices.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateHomeService(HomeService homeService, CancellationToken cancellationToken)
        {
            var homeServicey1 = await _appDbContext.HomeServices.FirstOrDefaultAsync(x => x.Id == homeService.Id, cancellationToken);
            homeServicey1.Id = homeService.Id;
            homeServicey1.Title = homeService.Title;
            homeServicey1.Description = homeService.Description;
            homeServicey1.BasePrice = homeService.BasePrice;
            homeServicey1.ImagePath = homeService.ImagePath;
            homeServicey1.NumberVisits = homeService.NumberVisits;
            homeServicey1.SubCategory= homeService.SubCategory;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
