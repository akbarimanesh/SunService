using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.UserS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.AppServices.SunServices.HService
{
    public class GetStatisticsDataExpertAppService : IGetStatisticsDataExpertAppService
    {
        private readonly IGetStatisticsDataExpertService _getStatisticsDataExpertService;

        public GetStatisticsDataExpertAppService(IGetStatisticsDataExpertService getStatisticsDataExpertService)
        {
            _getStatisticsDataExpertService = getStatisticsDataExpertService;
        }

        public async Task<int> GetBalanceCount(int ExpertId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataExpertService.GetBalanceCount(ExpertId, cancellationToken);
        }

        public async Task<int> GetOrderCount(int ExpertId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataExpertService.GetOrderCount(ExpertId, cancellationToken);
        }

        public async Task<int> GetServiceCount(CancellationToken cancellationToken)
        {
            return await _getStatisticsDataExpertService.GetServiceCount(cancellationToken);
        }

        public async Task<int> GetSkillCount(int ExpertId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataExpertService.GetSkillCount(ExpertId, cancellationToken);
        }
    }
}
