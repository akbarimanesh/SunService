using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Services.SunServices.HService
{
    public class GetStatisticsDataCustomerService : IGetStatisticsDataCustomerService
    {
        private readonly IGetStatisticsDataCustomerReopsitory _getStatisticsDataCustomerReopsitory;

        public GetStatisticsDataCustomerService(IGetStatisticsDataCustomerReopsitory getStatisticsDataCustomerReopsitory)
        {
            _getStatisticsDataCustomerReopsitory = getStatisticsDataCustomerReopsitory;
        }

        public async Task<int> GetBalanceCount(int customerId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataCustomerReopsitory.GetBalanceCount(customerId, cancellationToken);
        }

        public async Task<int> GetfferCount(int customerId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataCustomerReopsitory.GetfferCount(customerId, cancellationToken);
        }

        public async Task<int> GetOrderCount(int customerId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataCustomerReopsitory.GetOrderCount(customerId, cancellationToken);
        }

        public async Task<int> GetServiceCount(CancellationToken cancellationToken)
        {
            return await _getStatisticsDataCustomerReopsitory.GetServiceCount(cancellationToken);
        }
    }
}
