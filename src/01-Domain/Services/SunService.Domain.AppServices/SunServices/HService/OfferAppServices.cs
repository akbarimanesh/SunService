using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
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

        public async Task<List<OfferDto>> GetAllOffer(int OrderId, CancellationToken cancellationToken)
        {
            return await _offerServices.GetAllOffer(OrderId, cancellationToken);
            
        }

        public async Task<List<OfferDto>> GetAllOfferAllOrder(CancellationToken cancellationToken)
        {
            return await _offerServices.GetAllOfferAllOrder(cancellationToken);
        }
    }
}
