using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Domain.Core.SunServices.HService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Services.SunServices.HService
{
    public class orderServices : IorderServices
    { 
        private readonly IorderRepository _OrderRepository;

        public orderServices(IorderRepository orderRepository)
        {
            _OrderRepository = orderRepository;
        }

        public async Task CreateOrder(Order order, CancellationToken cancellationToken)
        {
            await _OrderRepository.CreateOrder(order, cancellationToken);
        }

        public async Task DeleteOrder(int id, CancellationToken cancellationToken)
        {
            await _OrderRepository.DeleteOrder(id, cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllOrder(CancellationToken cancellationToken)
        {
            return await _OrderRepository.GetAllOrder(cancellationToken);
        }

        public async Task<Order> GetorderById(int id, CancellationToken cancellationToken)
        {
            return await _OrderRepository.GetorderById(id, cancellationToken);
        }

        public async Task UpdateOrderStatus(int orderId, OrderHomeServiceStatusEnum newStatus, CancellationToken cancellationToken)
        {
            await _OrderRepository.UpdateOrderStatus(orderId, newStatus, cancellationToken);
        }
    }
}
