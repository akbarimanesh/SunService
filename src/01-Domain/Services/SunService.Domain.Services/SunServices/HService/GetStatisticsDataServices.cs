using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Services.SunServices.HService
{
    public class GetStatisticsDataServices : IGetStatisticsDataServices
    {
        private readonly IGetStatisticsDataReopsitory _getStatisticsDataReopsitory;

        public GetStatisticsDataServices(IGetStatisticsDataReopsitory getStatisticsDataReopsitory)
        {
            _getStatisticsDataReopsitory = getStatisticsDataReopsitory;
        }

        public async Task<int> GetCustomerCount(CancellationToken cancellationToken)
        {
            return await _getStatisticsDataReopsitory.GetCustomerCount(cancellationToken);
        }

        public async Task<int> GetExpertCount(CancellationToken cancellationToken)
        {
            return await _getStatisticsDataReopsitory.GetExpertCount(cancellationToken);
        }

        public async Task<int> GetOfferCount(CancellationToken cancellationToken)
        {
            return await _getStatisticsDataReopsitory.GetOfferCount(cancellationToken);
        }

        public async Task<int> GetOrderCount(CancellationToken cancellationToken)
        {
            return await _getStatisticsDataReopsitory.GetOrderCount(cancellationToken);
        }
    }
}
