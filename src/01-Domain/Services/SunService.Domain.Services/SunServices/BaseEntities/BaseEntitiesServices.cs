using SunService.Domain.Core.SunServices.BaseEntities.Data;
using SunService.Domain.Core.SunServices.BaseEntities.Services;

namespace SunService.Domain.Services.SunServices.BaseEntities
{
    public class BaseEntitiesServices : IBaseEntitiesServices
    {
        private readonly IBaseEntitiesRepository _BaseEntitiesRepository;
        public BaseEntitiesServices(IBaseEntitiesRepository baseEntitiesRepository)
        {
            _BaseEntitiesRepository = baseEntitiesRepository;
        }

        public async Task CreateCity(City city, CancellationToken cancellationToken)
        {
            await _BaseEntitiesRepository.CreateCity(city, cancellationToken);
        }

        public async Task DeleteCity(int id, CancellationToken cancellationToken)
        {
            await _BaseEntitiesRepository.DeleteCity(id, cancellationToken);
        }

        public async Task<List<City>> GetCities(CancellationToken cancellationToken)
        {
            return await _BaseEntitiesRepository.GetCities(cancellationToken);
        }

        public async Task<City> GetCityById(int id, CancellationToken cancellationToken)
        {
            return await _BaseEntitiesRepository.GetCityById(id, cancellationToken);
        }

        public async Task UpdateCity(City city, CancellationToken cancellationToken)
        {
             await _BaseEntitiesRepository.UpdateCity(city, cancellationToken);
        }
    }
}
