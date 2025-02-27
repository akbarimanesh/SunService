using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.Data
{
    public interface IGetStatisticsDataReopsitory
    {
        public Task<int> GetCustomerCount(CancellationToken cancellationToken);
        public Task<int> GetExpertCount(CancellationToken cancellationToken);
        public Task<int> GetOrderCount(CancellationToken cancellationToken);
        public Task<int> GetOfferCount(CancellationToken cancellationToken);

    }
}
