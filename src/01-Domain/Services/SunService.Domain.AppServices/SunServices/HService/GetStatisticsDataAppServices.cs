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
    public class GetStatisticsDataAppServices : IGetStatisticsDataAppServices
    {
        private readonly IGetStatisticsDataServices _getStatisticsDataServices;

        public GetStatisticsDataAppServices(IGetStatisticsDataServices getStatisticsDataServices)
        {
            _getStatisticsDataServices = getStatisticsDataServices;
        }

        public async Task<StatisticsDataDto> StatisticsDataCount(CancellationToken cancellationToken)
        {
            var model = new StatisticsDataDto();

            model.CustomerCount = await _getStatisticsDataServices.GetCustomerCount(cancellationToken);
            model.ExpertCount = await _getStatisticsDataServices.GetExpertCount(cancellationToken);
            model.OrderCount = await _getStatisticsDataServices.GetOrderCount(cancellationToken);
            model.OfferCount = await _getStatisticsDataServices.GetOfferCount(cancellationToken);
            return model;
        }
    }
}
