using SunService.Domain.Core.SunServices.HService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.AppServices
{
    public interface IGetStatisticsDataCustomerAppService
    {
        public Task<int> GetBalanceCount(int customerId, CancellationToken cancellationToken);
        public Task<int> GetOrderCount(int customerId, CancellationToken cancellationToken);
        public Task<int> GetfferCount(int customerId, CancellationToken cancellationToken);

        public Task<int> GetServiceCount(CancellationToken cancellationToken);
    }
}
