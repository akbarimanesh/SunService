using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.AppServices
{
    public interface IorderAppServices
    {
        public Task<List<OrderDto>> GetAllOrderUser(int id, CancellationToken cancellationToken);
        public Task<Order> GetorderById(int id, CancellationToken cancellationToken);
        public Task<List<OrderDto>> GetAllOrder(CancellationToken cancellationToken);
        public Task<Result> UpdateOrderStatus(int orderId, OrderHomeServiceStatusEnum newStatus, CancellationToken cancellationToken);
    }
}
