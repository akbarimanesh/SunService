

using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Domain.Core.SunServices.UserS.Entities;
using System.Threading;

namespace SunService.Domain.Core.SunServices.HService.Data
{
    public interface IorderRepository
    {
        public Task<List<OrderDto>> GetAllOrder(CancellationToken cancellationToken);
        public Task<List<OrderDto>> GetAllOrderUser(int id,CancellationToken cancellationToken);
        public Task<Order> GetorderById(int id, CancellationToken cancellationToken);
        public Task<int> CreateOrder(OrderDto orderdto, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteOrder(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateOrderStatus(int orderId, OrderHomeServiceStatusEnum newStatus, CancellationToken cancellationToken);
        public Task<bool> HasExpertOffers(int orderId, CancellationToken cancellationToken);
        public Task<bool> HasCustomerChosenExpert(int orderId, CancellationToken cancellationToken);
    }
}
