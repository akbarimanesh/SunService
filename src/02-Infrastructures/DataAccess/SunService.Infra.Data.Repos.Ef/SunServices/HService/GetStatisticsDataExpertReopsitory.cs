using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Repos.Ef.SunServices.HService
{
    public class GetStatisticsDataExpertReopsitory : IGetStatisticsDataExpertReopsitory
    {
        private readonly AppDbContext _appDbContext;

        public GetStatisticsDataExpertReopsitory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> GetBalanceCount(int ExpertId, CancellationToken cancellationToken)
        {
            return await _appDbContext.Experts
                  .Where(c => c.Id == ExpertId)
                  .Select(c => c.Balance ?? 0)
                  .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> GetOrderCount(int ExpertId, CancellationToken cancellationToken)
        {
            var expertSkills = await _appDbContext.ExpertServices
           .Where(es => es.ExpertId == ExpertId)
           .Select(es => es.HomeServiceId)
           .ToListAsync(cancellationToken);

            if (!expertSkills.Any())
            {
                return 0;
            }

            return await _appDbContext.Orders
                .Where(o => expertSkills.Contains(o.HomeServiceId))
                .CountAsync(cancellationToken);
        }

        public async Task<int> GetServiceCount(CancellationToken cancellationToken)
        {
            return await _appDbContext.HomeServices

               .CountAsync(cancellationToken);
        }

        public async Task<int> GetSkillCount(int ExpertId, CancellationToken cancellationToken)
        {
            return await _appDbContext.ExpertServices
            .Where(es => es.ExpertId == ExpertId)
           .Select(es => es.HomeServiceId)
           .Distinct()
            .CountAsync(cancellationToken);
        }
    }
}
