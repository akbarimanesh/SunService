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
    public class GetStatisticsDataReopsitory : IGetStatisticsDataReopsitory
    {
        private readonly AppDbContext _appDbContext;

        public GetStatisticsDataReopsitory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> GetCustomerCount(CancellationToken cancellationToken)
        {
            
            var x = await _appDbContext.UserClaims.AsNoTracking().Where(x => x.ClaimValue== "Customer").CountAsync(cancellationToken);
            return x;
        }

        public async Task<int> GetExpertCount(CancellationToken cancellationToken)
        {
            var x = await _appDbContext.UserClaims.AsNoTracking().Where(x => x.ClaimValue == "Expert").CountAsync(cancellationToken);
            return x;
        }

        public async Task<int> GetOfferCount(CancellationToken cancellationToken)
        {
            var x = await _appDbContext.Offers.AsNoTracking().CountAsync(cancellationToken);
            return x;
        }

        public async Task<int> GetOrderCount(CancellationToken cancellationToken)
        {
            var x = await _appDbContext.Orders.AsNoTracking().CountAsync(cancellationToken);
            return x;
        }
    }
}
