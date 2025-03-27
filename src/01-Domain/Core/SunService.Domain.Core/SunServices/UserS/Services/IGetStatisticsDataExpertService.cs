using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.Services
{
    public interface IGetStatisticsDataExpertService
    {
        public Task<int> GetBalanceCount(int ExpertId, CancellationToken cancellationToken);
        public Task<int> GetOrderCount(int ExpertId, CancellationToken cancellationToken);
        public Task<int> GetSkillCount(int ExpertId, CancellationToken cancellationToken);

        public Task<int> GetServiceCount(CancellationToken cancellationToken);
    }
}
