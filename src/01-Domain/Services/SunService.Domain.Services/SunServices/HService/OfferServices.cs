using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Services.SunServices.HService
{
    public class OfferServices : IOfferServices
    {
        private readonly IOfferRepository _OfferRepository;

        public OfferServices(IOfferRepository offerRepository)
        {
            _OfferRepository = offerRepository;
        }

        public async Task CreateOffer(Offer offer, CancellationToken cancellationToken)
        {
            await _OfferRepository.CreateOffer(offer, cancellationToken);
        }

        public async Task DeleteOffer(int id, CancellationToken cancellationToken)
        {
            await _OfferRepository.DeleteOffer(id, cancellationToken);
        }

        public async Task<List<OfferDto>> GetAllOffer(CancellationToken cancellationToken)
        {
           return await _OfferRepository.GetAllOffer(cancellationToken);
        }

        public async Task<Offer> GetOfferById(int id, CancellationToken cancellationToken)
        {
            return await _OfferRepository.GetOfferById(id, cancellationToken);
        }

        public async Task UpdateOffer(Offer offer, CancellationToken cancellationToken)
        {
             await _OfferRepository.UpdateOffer(offer, cancellationToken);
        }
    }
}
