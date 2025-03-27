using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.UserS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Services.SunServices.HService
{
    public class GetStatisticsDataExpertService : IGetStatisticsDataExpertService
    {
        private readonly IGetStatisticsDataExpertReopsitory _getStatisticsDataExpertReopsitory;

        public GetStatisticsDataExpertService(IGetStatisticsDataExpertReopsitory getStatisticsDataExpertReopsitory)
        {
            _getStatisticsDataExpertReopsitory = getStatisticsDataExpertReopsitory;
        }

        public async Task<int> GetBalanceCount(int ExpertId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataExpertReopsitory.GetBalanceCount(ExpertId, cancellationToken);
        }

        public async Task<int> GetOrderCount(int ExpertId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataExpertReopsitory.GetOrderCount(ExpertId, cancellationToken);
        }

        public async Task<int> GetServiceCount(CancellationToken cancellationToken)
        {
            return await _getStatisticsDataExpertReopsitory.GetServiceCount(cancellationToken);
        }

        public async Task<int> GetSkillCount(int ExpertId, CancellationToken cancellationToken)
        {
            return await _getStatisticsDataExpertReopsitory.GetSkillCount(ExpertId, cancellationToken);
        }
    }
}
