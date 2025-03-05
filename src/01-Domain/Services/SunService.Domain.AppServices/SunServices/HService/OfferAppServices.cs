using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Services;
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
    }
}
