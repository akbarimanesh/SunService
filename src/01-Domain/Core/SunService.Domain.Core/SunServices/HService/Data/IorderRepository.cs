

using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;

namespace SunService.Domain.Core.SunServices.HService.Data
{
    public interface IorderRepository
    {
        public Task<List<OrderDto>> GetAllOrder(CancellationToken cancellationToken);
        public Task<Order> GetorderById(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateOrder(Order order, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteOrder(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateOrderStatus(int orderId, OrderHomeServiceStatusEnum newStatus, CancellationToken cancellationToken);
    }
}
