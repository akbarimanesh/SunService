using SunService.Domain.Core.SunServices.BaseEntities.AppServices;
using SunService.Domain.Core.SunServices.BaseEntities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.AppServices.SunServices.BaseEntities
{
    public class BaseDataAppService : IBaseDataAppService
    {
        private readonly IBaseEntitiesServices _baseEntitiesServices;

        public BaseDataAppService(IBaseEntitiesServices baseEntitiesServices)
        {
            _baseEntitiesServices = baseEntitiesServices;
        }

        public async Task<List<City>> GetCities(CancellationToken cancellationToken)
        {
            return await _baseEntitiesServices.GetCities(cancellationToken);
        }
    }
}
