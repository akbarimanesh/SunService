using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.Task.Configs;
using SunService.Infra.Data.Db.SqlServer.Dapper;
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
        private readonly string _connectionString;
        public HomeServiceRepository(IOptions<SiteSettings> siteSettings, AppDbContext appDbContext)
        {
            _connectionString = siteSettings.Value.ConnectionStrings.SqlConnection;
            _appDbContext = appDbContext;
        }
      
        public async Task CreateHomeService(HomeServiceDto homeService, CancellationToken cancellationToken)
        {
            var homeService1 = new HomeService
            {
                Title = homeService.Title,
                Description= homeService.Description,
                BasePrice = homeService.BasePrice,
                SubCategoryId= homeService.SubCategoryId,
                ImagePath = homeService.ImagePath

            };
            await _appDbContext.HomeServices.AddAsync(homeService1, cancellationToken);
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
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var homeservices = await connection.QueryAsync<HomeServiceDto>(QuerysSundb.GetAllHomeServices);
                return homeservices.ToList();
            }
        }
        //public async Task<List<HomeServiceDto>> GetAllHomeService(CancellationToken cancellationToken)
        //{
        //    return await _appDbContext.HomeServices.AsNoTracking().Include(h => h.SubCategory).ThenInclude(sc => sc.Category).Select(x => new HomeServiceDto()
        //    {
        //        Id=x.Id,
        //        Title = x.Title,
        //        Description = x.Description,
        //        BasePrice = x.BasePrice,
        //        ImagePath = x.ImagePath,
        //        NumberVisits = x.NumberVisits,
        //        SubCategoryTitle=x.SubCategory.Title,
        //     SubCategoryId=x.SubCategoryId,
        //      CategoryTitle = x.SubCategory.Category.Title,
        //    }).ToListAsync(cancellationToken);
        //}

        public async Task<HomeService> GetHomeServiceById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.HomeServices.AsNoTracking().Include(h => h.SubCategory).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<HomeServiceDto>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)
        {
            return await _appDbContext.HomeServices
            .Where(hs => hs.SubCategoryId == subCategoryId) 
            .Select(hs => new HomeServiceDto
            {
                Id = hs.Id,
                Title = hs.Title,
                SubCategoryTitle = hs.SubCategory.Title, 
                BasePrice = hs.BasePrice,
                ImagePath = hs.ImagePath
            })
            .ToListAsync(cancellationToken); 
        }

        public async Task<bool> GetTitleHomeService(string homeServiceTitle, CancellationToken cToken)
        {
            return await _appDbContext.HomeServices.AsNoTracking().AnyAsync(t => t.Title == homeServiceTitle);
        }

        public async Task UpdateExpertServices(int expertId, List<int> selectedHomeServices, CancellationToken cancellationToken)
        {
            var existingServices = await _appDbContext.ExpertServices
                .Where(es => es.ExpertId == expertId)
                .ToListAsync(cancellationToken);

            
            var servicesToRemove = existingServices.Where(es => !selectedHomeServices.Contains(es.HomeServiceId)).ToList();
            _appDbContext.ExpertServices.RemoveRange(servicesToRemove);

          
            var existingServiceIds = existingServices.Select(es => es.HomeServiceId).ToList();
            var newServices = selectedHomeServices.Where(sid => !existingServiceIds.Contains(sid))
                .Select(sid => new ExpertService { ExpertId = expertId, HomeServiceId = sid }).ToList();

            await _appDbContext.ExpertServices.AddRangeAsync(newServices, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateHomeService(HomeServiceDto homeService, CancellationToken cancellationToken)
        {
            var homeServicey1 = await _appDbContext.HomeServices.FirstOrDefaultAsync(x => x.Id == homeService.Id, cancellationToken);
            homeServicey1.Id = homeService.Id;
            homeServicey1.Title = homeService.Title;
            homeServicey1.Description = homeService.Description;
            homeServicey1.BasePrice = homeService.BasePrice;
            homeServicey1.ImagePath = homeService.ImagePath ?? homeServicey1.ImagePath;


            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
