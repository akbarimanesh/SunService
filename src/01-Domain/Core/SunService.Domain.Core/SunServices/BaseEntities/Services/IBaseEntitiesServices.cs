using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.BaseEntities.Services
{
    public interface IBaseEntitiesServices
    {
        Task<List<City>> GetCities(CancellationToken cancellationToken);
        public Task<City> GetCityById(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateCity(City city, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteCity(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateCity(City city, CancellationToken cancellationToken);
    }
}
