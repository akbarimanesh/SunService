using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SunService.Domain.Core.SunServices.BaseEntities.Data;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.Task.Configs;
using SunService.Infra.Data.Db.SqlServer.Dapper;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using SunService.Infra.Data.Db.SqlServer.Dapper;
using Dapper;
using Microsoft.Extensions.Options;
namespace SunService.Infra.Data.Repos.Ef.SunServices.BaseEntities
{
    public class BaseEntitiesRepository : IBaseEntitiesRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly string _connectionString;
        public BaseEntitiesRepository(IOptions<SiteSettings> siteSettings, AppDbContext appDbContext)
        {
            _connectionString = siteSettings.Value.ConnectionStrings.SqlConnection;
            _appDbContext = appDbContext;
        }
       
        public async Task AddOrderImages(List<string> imgAddress, int orderId, CancellationToken cancellationToken)
        {
            var images = imgAddress.Select(x => new Image()
            {
                Path = x,
                OrderId = orderId
            });

            await _appDbContext.Images.AddRangeAsync(images, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateCity(City city, CancellationToken cancellationToken)
        {
            await _appDbContext.Cities.AddAsync(city, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCity(int id, CancellationToken cancellationToken)
        {
            var city = await _appDbContext.Cities.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Cities.Remove(city);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<City>> GetCities(CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return (await connection.QueryAsync<City>(QuerysSundb.GetAllcities)).ToList();
            }
        }

        //public async Task<List<City>> GetCities(CancellationToken cancellationToken)
        //{
        //    return await _appDbContext.Cities.AsNoTracking().ToListAsync(cancellationToken);
        //}

        public async Task<City> GetCityById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Cities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateCity(City city, CancellationToken cancellationToken)
        {
            var city1 = await _appDbContext.Cities.FirstOrDefaultAsync(x => x.Id == city.Id, cancellationToken);
            city1.Id = city.Id;
            city1.Title = city.Title;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
