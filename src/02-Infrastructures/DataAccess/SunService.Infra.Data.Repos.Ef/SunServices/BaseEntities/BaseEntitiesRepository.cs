using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.BaseEntities.Data;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Repos.Ef.SunServices.BaseEntities
{
    public class BaseEntitiesRepository : IBaseEntitiesRepository
    {
        private readonly AppDbContext _appDbContext;

        public BaseEntitiesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
            return await _appDbContext.Cities.AsNoTracking().ToListAsync(cancellationToken);
        }

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
