using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.BaseEntities.AppServices
{
    public interface IBaseDataAppService
    {
        Task<List<City>> GetCities(CancellationToken cancellationToken);
    }
}
