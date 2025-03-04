using Framework;
using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;


namespace SunService.Infra.Data.Repos.Ef.SunServices.HService
{
    public class orderRepository : IorderRepository
    {
        private readonly AppDbContext _appDbContext;

        public orderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> CreateOrder(OrderDto order, CancellationToken cancellationToken)
        {

            var order1 = new Order()
            {
                Id = order.Id,
                ImplementationTime = order.ImplementationTime,
                ImplementationDate = order.ImplementationDate,

                Description = order.Description,
                CreateAt = DateTime.Now,
                OrderHomeServiceStatus = OrderHomeServiceStatusEnum.OfferExpert,
                OfferId = order.OfferId,

                CityId = order.CityId ?? 0,


                CustomerId = order.CustomerId,
             
               HomeServiceId = order.HomeserviceId,
                ExpertId = order.ExpertId,

            };
            await _appDbContext.Orders.AddAsync(order1, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return order1.Id;
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
                CustomerFullName = x.Customer.FirstName + " " + x.Customer.LastName,
                ImplementationDate = x.ImplementationDate,
                ImplementationTime = x.ImplementationTime ,
                CreateAt = x.CreateAt,
                OrderHomeServiceStatus = x.OrderHomeServiceStatus


            }).ToListAsync(cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllOrderUser(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Orders.AsNoTracking().Where(x => x.CustomerId == id).Select(x => new OrderDto()
            {
                Id = x.Id,
                HomeServiceTitle = x.HomeService.Title,
                CustomerFullName = x.Customer.FirstName + " " + x.Customer.LastName,
                ImplementationDate = x.ImplementationDate,
                ImplementationTime = x.ImplementationTime,
                CreateAt = x.CreateAt,
                OrderHomeServiceStatus = x.OrderHomeServiceStatus


            }).ToListAsync(cancellationToken);
        }

        public async Task<Order> GetorderById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Orders.AsNoTracking().Include(x => x.HomeService).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> HasCustomerChosenExpert(int orderId, CancellationToken cancellationToken)
        {
           return await _appDbContext.Offers.AsNoTracking().AnyAsync(x => x.OrderId == orderId && x.StateOffer==true, cancellationToken);
           
        }

        public async Task<bool> HasExpertOffers(int orderId, CancellationToken cancellationToken)
        {
           return await _appDbContext.Offers.AsNoTracking().AnyAsync(x=>x.OrderId==orderId, cancellationToken);
            
        }

        public async Task UpdateOrderStatus(int orderId, OrderHomeServiceStatusEnum newStatus, CancellationToken cancellationToken)
        {
            var order1 = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);
            
            order1.OrderHomeServiceStatus = newStatus;
            
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
