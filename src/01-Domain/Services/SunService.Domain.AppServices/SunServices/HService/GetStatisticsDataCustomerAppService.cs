using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.AppServices.SunServices.HService
{
    public class GetStatisticsDataCustomerAppService : IGetStatisticsDataCustomerAppService
    {
        private readonly IGetStatisticsDataCustomerService _getStatisticsDataCustomerService;

        public GetStatisticsDataCustomerAppService(IGetStatisticsDataCustomerService getStatisticsDataCustomerService)
        {
            _getStatisticsDataCustomerService = getStatisticsDataCustomerService;
        }

        public async Task<int> GetBalanceCount(int customerId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataCustomerService.GetBalanceCount(customerId, cancellationToken);
        }

        public async Task<int> GetfferCount(int customerId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataCustomerService.GetfferCount(customerId, cancellationToken);
        }

        public async Task<int> GetOrderCount(int customerId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataCustomerService.GetOrderCount(customerId, cancellationToken);
        }

        public async Task<int> GetServiceCount(CancellationToken cancellationToken)
        {
            return await _getStatisticsDataCustomerService.GetServiceCount(cancellationToken);
        }
    }
}
