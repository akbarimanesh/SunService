using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Services;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.AppServices.SunServices.HService
{
    public class OfferAppServices : IOfferAppServices
    {
        private readonly IOfferServices _offerServices;
       
        public OfferAppServices(IOfferServices offerServices)
        {
            _offerServices = offerServices;
           
        }

        public async Task<Result> AcceptOffer(int id, CancellationToken cToken)
        {
            await _offerServices.AcceptOffer(id, cToken);
            return new Result(true, " پیشنهاد مورد نظر شما با موفقیت پذیرفته شد .");
        }

        public async Task<List<OfferDto>> GetAllOffer(int OrderId, CancellationToken cancellationToken)
        {
            return await _offerServices.GetAllOffer(OrderId, cancellationToken);
            
        }

        public async Task<List<OfferDto>> GetAllOfferAllOrder(CancellationToken cancellationToken)
        {
            return await _offerServices.GetAllOfferAllOrder(cancellationToken);
        }

        public async Task<Offer> GetOfferById(int id, CancellationToken cancellationToken)
        {
            if (await _offerServices.GetOfferById(id, cancellationToken) != null)
            {
                return await _offerServices.GetOfferById(id, cancellationToken);

            }
            else
                return null;
        }

        public async Task<Result> RejectedOffer(int id, CancellationToken cToken)
        {
            await _offerServices.RejectedOffer(id, cToken);
            return new Result(true, "رد شد.");
        }

        public async Task<Result> UpdateBalances(int offerId, CancellationToken cancellationToken)
        {   
            
            var offer1 = await _offerServices.GetOfferById(offerId, cancellationToken);
            if (offer1 == null) 
            return new Result(true, "پیشنهاد یافت نشد.");
          
           var customerId = offer1.Order.CustomerId;
            var customer = await _offerServices.GetCustomer(customerId, cancellationToken);

            
            var expert = offer1.Order.Expert;
          
            var admin = await _offerServices.GetAdmin(cancellationToken);
            

            if (customer.Balance < offer1.PriceOffer)
            {
                return new Result(true, "موجودی مشتری کافی نیست. لطفاً موجودی خود را افزایش دهید.");
            }

            int adminShare = (offer1.PriceOffer * 30) / 100;
            int expertShare = offer1.PriceOffer - adminShare;

            
            customer.Balance -= offer1.PriceOffer;
            var newBalanceCustomer = customer.Balance ?? 0;
            expert.Balance += expertShare;
            var newBalanceExpert = expert.Balance ?? 0;
            admin.Balance += adminShare;
            var newBalanceAdmin= admin.Balance ?? 0;
            await _offerServices.UpdateUserBalance(customer.Id, newBalanceCustomer, cancellationToken);
            await _offerServices.UpdateUserBalance(expert.Id, newBalanceExpert, cancellationToken);
            await _offerServices.UpdateUserBalance(admin.Id, newBalanceAdmin , cancellationToken);
            await _offerServices.ChangeStatuseOrder(offer1.OrderId, cancellationToken);
            return new Result(true, "پرداخت و واریز با موفقیت انجام شد.");
        }
    }
}
   

