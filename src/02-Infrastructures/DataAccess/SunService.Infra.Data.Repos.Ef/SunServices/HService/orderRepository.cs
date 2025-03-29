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

        public async Task ActiveOrder(int orderid, CancellationToken cToken)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderid, cToken);
            order.StateOrder = true;
            await _appDbContext.SaveChangesAsync(cToken);
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
                
               Offers=order.Offers,
                CustomerId = order.CustomerId,
             StateOrder=order.StateOrder,
               HomeServiceId = order.HomeserviceId,
                ExpertId = order.ExpertId,

            };
            await _appDbContext.Orders.AddAsync(order1, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return order1.Id;
        }

        public async Task DeActiveOrder(int orderid, CancellationToken cToken)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderid, cToken);
            order.StateOrder = false;
            await _appDbContext.SaveChangesAsync(cToken);
        }

        public async Task DeleteOrder(int id, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllOrder(CancellationToken cancellationToken)
        {
            return await _appDbContext.Orders.AsNoTracking().Include(o => o.Offers).OrderByDescending(o => o.CreateAt).Select(x => new OrderDto()
            {
                Id = x.Id,
                HomeServiceTitle = x.HomeService.Title,
                CustomerFullName = x.Customer.FirstName + " " + x.Customer.LastName,
                ImplementationDate = x.ImplementationDate,
                ImplementationTime = x.ImplementationTime ,
                CreateAt = x.CreateAt,
                OrderHomeServiceStatus = x.OrderHomeServiceStatus,
                 OfferId = x.OfferId,
                Offers=x.Offers.ToList(),
                StateOrder=x.StateOrder
            }).ToListAsync(cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllOrderHomeserviceExpert(int expertId, CancellationToken cancellationToken)
        {
            var expertSkills = await _appDbContext.ExpertServices
           .Where(es => es.ExpertId == expertId)
           .Select(es => es.HomeServiceId)
           .ToListAsync(cancellationToken);

            if (!expertSkills.Any())
            {
                return new List<OrderDto>(); 
            }

            
            var orders = await _appDbContext.Orders
                .Where(o => expertSkills.Contains(o.HomeServiceId)  ).Include(o => o.Offers)
                .OrderByDescending(o => o.CreateAt)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    HomeServiceTitle = o.HomeService.Title,
                    CreateAt = o.CreateAt,
                    ImplementationDate = o.ImplementationDate,
                    OrderHomeServiceStatus = o.OrderHomeServiceStatus,
                    Description = o.Description,
                    CustomerFullName = o.Customer.FirstName + " " + o.Customer.LastName, 
                    CityId = o.Customer.CityId,
                    ImageUrls = o.Images.Select(img => img.Path).ToList(),
                    Offers = o.Offers.ToList(),
                    StateOrder=o.StateOrder
                })
                .ToListAsync(cancellationToken);

            return orders;
        }
        

        public async Task<List<OrderDto>> GetAllOrderUser(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Orders.AsNoTracking().Where(x => x.CustomerId == id).Include(o => o.Offers).OrderByDescending(o => o.CreateAt).Select(x => new OrderDto()
            {
                Id = x.Id,
                HomeServiceTitle = x.HomeService.Title,
                CustomerFullName = x.Customer.FirstName + " " + x.Customer.LastName,
                ImplementationDate = x.ImplementationDate,
                ImplementationTime = x.ImplementationTime,
                CreateAt = x.CreateAt,
                OrderHomeServiceStatus = x.OrderHomeServiceStatus,
                OfferId = x.OfferId,
                Offers = x.Offers.ToList(),
                StateOrder=x.StateOrder
            }).ToListAsync(cancellationToken);
        }

        public async Task<Order> GetorderById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Orders.AsNoTracking().Include(x => x.HomeService).Include(x => x.Images).Include(o => o.Offers).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
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
