using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
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

        public async Task UpdateOrder(Order order, CancellationToken cancellationToken)
        {
            var order1 = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == order.Id, cancellationToken);
            order1.Id = order.Id;
            order1.Description = order.Description;
            order1.ImplementationDate= order.ImplementationDate;
            order1.ImplementationTime= order.ImplementationTime;
            order1.CityId= order.CityId;
            order1.StateOrder= order.StateOrder;
            order1.OrderHomeServiceStatus = order.OrderHomeServiceStatus;
            order1.OfferId= order.OfferId;
            order1.CustomerId= order.CustomerId;
            order1.ExpertId= order.ExpertId;
            order1.HomeServiceId= order.HomeServiceId;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
