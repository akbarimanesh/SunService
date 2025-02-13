using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.BaseEntities.Data
{
    public interface IBaseEntitiesRepository
    {
        Task<List<City>> GetCities(CancellationToken cancellationToken);
        public Task<City> GetCityById(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateCity(City city, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteCity(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateCity(City city, CancellationToken cancellationToken);

    }
}
