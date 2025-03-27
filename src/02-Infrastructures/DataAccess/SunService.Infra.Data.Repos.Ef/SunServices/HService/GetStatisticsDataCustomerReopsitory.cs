using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Repos.Ef.SunServices.HService
{
    public class GetStatisticsDataCustomerReopsitory : IGetStatisticsDataCustomerReopsitory
    {
        private readonly AppDbContext _appDbContext;

        public GetStatisticsDataCustomerReopsitory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> GetBalanceCount(int customerId, CancellationToken cancellationToken)
        {
            return await _appDbContext.Customers
                  .Where(c => c.Id == customerId)
                  .Select(c => c.Balance ?? 0)
                  .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> GetfferCount(int customerId, CancellationToken cancellationToken)
        {
            return await _appDbContext.Offers
                 .Where(o => o.Order.CustomerId == customerId)
                .CountAsync(cancellationToken);
        }

        public async Task<int> GetOrderCount(int customerId, CancellationToken cancellationToken)
        {
            return await _appDbContext.Orders
                .Where(o => o.CustomerId == customerId)
                .CountAsync(cancellationToken);
        }

        public async Task<int> GetServiceCount( CancellationToken cancellationToken)
        {
            return await _appDbContext.HomeServices
                
                .CountAsync(cancellationToken);
        }
    }
}
