using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.Services
{
    public interface IorderServices
    {
        public Task<List<OrderDto>> GetAllOrder(CancellationToken cancellationToken);
        public Task<Order> GetorderById(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateOrder(Order order, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteOrder(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateOrder(Order order, CancellationToken cancellationToken);
    }
}
