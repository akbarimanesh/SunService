using SunService.Domain.Core.SunServices.BaseEntities.Services;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Domain.Core.SunServices.HService.Services;
using SunService.Domain.Core.SunServices.UserS.Services;
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
        private readonly IBaseEntitiesServices _baseEntitiesServices;
        private readonly ICustomerServices _customerServices;
        private readonly IExpertServices _expertServices;
        public orderAppServices(IorderServices orderServices, ICustomerServices customerServices, IExpertServices expertServices = null)
        {
            _orderServices = orderServices;
            _customerServices = customerServices;
            _expertServices = expertServices;
        }

        public async Task<Result> CreateOrder(OrderDto orderdto, CancellationToken cancellationToken)
        {
            var customer = await _customerServices.GetCustomerById(orderdto.CustomerId, cancellationToken);
            if (customer == null)
            {
                return new Result(false, "مشتری یافت نشد.");
            }

            
            var expert = await _expertServices.GetExpertById(orderdto.HomeserviceId, cancellationToken);
            if (expert == null)
            {
                return new Result(false, "کارشناسی برای این خدمت یافت نشد.");
                
            }

            
            if (customer.CityId != expert.CityId)
            {
                return new Result(false, "خدمات مورد نظر برای شهر شما در دسترس نیست.");
               
            }
            var imagesPath = new List<string>();
            var orderid = await _orderServices.CreateOrder(orderdto,cancellationToken);
           
            if (orderdto.Images is not null)
            {
                foreach (var image in orderdto.Images)
                {
                    var imagePath = await _baseEntitiesServices.UploadImage(image, "Order", cancellationToken);
                    imagesPath.Add(imagePath);
                }

                await _baseEntitiesServices.AddOrderImages(imagesPath,orderid, cancellationToken);
            }
            return new Result(true, "سفارش شما با موفقیت ثبت شد");
        }

        public async Task<List<OrderDto>> GetAllOrder(CancellationToken cancellationToken)
        {
            return await _orderServices.GetAllOrder(cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllOrderUser(int id, CancellationToken cancellationToken)
        {
            return await _orderServices.GetAllOrderUser(id, cancellationToken);
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
            var order = await _orderServices.GetorderById(orderId, cancellationToken);
            var allowedStatuses = GetAllowedStatuses(order.OrderHomeServiceStatus);
            if (order.OrderHomeServiceStatus == OrderHomeServiceStatusEnum.OfferExpert && newStatus == OrderHomeServiceStatusEnum.ChoiceExpert)
            {
                var hasExpertOffers = await _orderServices.HasExpertOffers(orderId, cancellationToken);
                if (!hasExpertOffers)
                {
                    return new Result(false, "امکان تغییر وضعیت وجود ندارد! هنوز کارشناسی پیشنهادی نداده است.");
                }
            }

           
            if (order.OrderHomeServiceStatus == OrderHomeServiceStatusEnum.ChoiceExpert && newStatus == OrderHomeServiceStatusEnum.ExpetToCome)
            {
                var hasCustomerChosenExpert = await _orderServices.HasCustomerChosenExpert(orderId, cancellationToken);
                if (!hasCustomerChosenExpert)
                {
                    return new Result(false, "امکان تغییر وضعیت وجود ندارد! مشتری هنوز کارشناسی را انتخاب نکرده است.");
                }
            }

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
                    return new List<OrderHomeServiceStatusEnum> { OrderHomeServiceStatusEnum.OfferExpert, OrderHomeServiceStatusEnum.ChoiceExpert, OrderHomeServiceStatusEnum.ExpetToCome };

                case OrderHomeServiceStatusEnum.ExpetToCome:
                    return new List<OrderHomeServiceStatusEnum> { OrderHomeServiceStatusEnum.ChoiceExpert, OrderHomeServiceStatusEnum.ExpetToCome, OrderHomeServiceStatusEnum.FinishingWork };

                case OrderHomeServiceStatusEnum.FinishingWork:
                    return new List<OrderHomeServiceStatusEnum> { OrderHomeServiceStatusEnum.ExpetToCome, OrderHomeServiceStatusEnum.FinishingWork };

                default:
                    return new List<OrderHomeServiceStatusEnum> { currentStatus };
            }
        }
    }
}
 
