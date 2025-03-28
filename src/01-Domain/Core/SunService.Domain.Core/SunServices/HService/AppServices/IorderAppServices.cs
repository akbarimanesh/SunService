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
        public Task<Result> ActiveOrder(int orderid, CancellationToken cToken);
        public Task<Result> DeActiveOrder(int orderid, CancellationToken cToken);
        public  Task<List<OrderDto>> GetAllOrderHomeserviceExpert(int expertId, CancellationToken cancellationToken);
        public Task<List<OrderDto>> GetAllOrderUser(int id, CancellationToken cancellationToken);
        public Task<Order> GetorderById(int id, CancellationToken cancellationToken);
        public Task<List<OrderDto>> GetAllOrder(CancellationToken cancellationToken);
        public Task<Result> CreateOrder(OrderDto orderdto, CancellationToken cancellationToken);
        public Task<Result> UpdateOrderStatus(int orderId, OrderHomeServiceStatusEnum newStatus, CancellationToken cancellationToken);
    }
}
