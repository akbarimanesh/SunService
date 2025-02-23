using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Domain.Core.SunServices.HService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.AppServices.SunServices.HService
{
    public class orderAppServices : IorderAppServices
    {
        private readonly IorderServices _orderServices;

        public orderAppServices(IorderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public async Task<List<OrderDto>> GetAllOrder(CancellationToken cancellationToken)
        {
            return await _orderServices.GetAllOrder(cancellationToken);
        }

        public async Task<Order> GetorderById(int id, CancellationToken cancellationToken)
        {
            if (await _orderServices.GetorderById(id, cancellationToken) != null)
            {
                return await _orderServices.GetorderById(id, cancellationToken);

            }
            else
                return null;
        }

        public async Task<Result> UpdateOrderStatus(int orderId, OrderHomeServiceStatusEnum newStatus, CancellationToken cancellationToken)
        {
            var order = await _orderServices.GetorderById(orderId,cancellationToken);
            var allowedStatuses = GetAllowedStatuses(order.OrderHomeServiceStatus);
            if (!allowedStatuses.Contains(newStatus))
            {
                return new Result(false, "امکان تغییر به این وضعیت وجود ندارد!");
            }
            await _orderServices.UpdateOrderStatus(orderId, newStatus, cancellationToken);
            return new Result(true, "وضعیت سفارش با موفقیت تغییر کرد.");
        }
        private List<OrderHomeServiceStatusEnum> GetAllowedStatuses(OrderHomeServiceStatusEnum currentStatus)
        {
            switch (currentStatus)
            {
                case OrderHomeServiceStatusEnum.OfferExpert:
                    return new List<OrderHomeServiceStatusEnum> { OrderHomeServiceStatusEnum.OfferExpert, OrderHomeServiceStatusEnum.ChoiceExpert };

                case OrderHomeServiceStatusEnum.ChoiceExpert:
                    return new List<OrderHomeServiceStatusEnum> { OrderHomeServiceStatusEnum.ChoiceExpert, OrderHomeServiceStatusEnum.ExpetToCome };

                case OrderHomeServiceStatusEnum.ExpetToCome:
                    return new List<OrderHomeServiceStatusEnum> { OrderHomeServiceStatusEnum.ExpetToCome, OrderHomeServiceStatusEnum.FinishingWork };

                case OrderHomeServiceStatusEnum.FinishingWork:
                    return new List<OrderHomeServiceStatusEnum> { OrderHomeServiceStatusEnum.FinishingWork };

                default:
                    return new List<OrderHomeServiceStatusEnum> { currentStatus };
            }
        }
    }
}

