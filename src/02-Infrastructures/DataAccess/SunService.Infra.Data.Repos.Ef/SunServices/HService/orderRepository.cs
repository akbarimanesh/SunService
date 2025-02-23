using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Repos.Ef.SunServices.HService
{
    public class orderRepository : IorderRepository
    {
        private readonly AppDbContext _appDbContext;

        public orderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateOrder(Order order, CancellationToken cancellationToken)
        {
            await _appDbContext.Orders.AddAsync(order, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteOrder(int id, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllOrder(CancellationToken cancellationToken)
        {
            return await _appDbContext.Orders.AsNoTracking().Select(x => new OrderDto()
            {
                Id = x.Id,
                HomeServiceTitle = x.HomeService.Title,
                CustomerFullName = x.Customer.FirstName + " " + x.Expert.LastName,
                ImplementationDate = x.ImplementationDate,
                ImplementationTime = x.ImplementationTime,
                CreateAt = x.CreateAt,
                OrderHomeServiceStatus = x.OrderHomeServiceStatus


            }).ToListAsync(cancellationToken);
        }

        public async Task<Order> GetorderById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateOrderStatus(int orderId, OrderHomeServiceStatusEnum newStatus, CancellationToken cancellationToken)
        {
            var order1 = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);
            
            order1.OrderHomeServiceStatus = newStatus;
            
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
