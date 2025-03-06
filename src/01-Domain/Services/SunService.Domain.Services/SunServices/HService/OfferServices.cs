using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Services;
using SunService.Domain.Core.SunServices.UserS.Entities;
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

        public async Task AcceptOffer(int id, CancellationToken cToken)
        {
            await _OfferRepository.AcceptOffer(id, cToken);
        }

        public async Task ChangeStatuseOrder(int orderId, CancellationToken cancellationToken)
        {
            await _OfferRepository.ChangeStatuseOrder(orderId, cancellationToken);
        }

        public async Task CreateOffer(Offer offer, CancellationToken cancellationToken)
        {
            await _OfferRepository.CreateOffer(offer, cancellationToken);
        }

        public async Task DeleteOffer(int id, CancellationToken cancellationToken)
        {
            await _OfferRepository.DeleteOffer(id, cancellationToken);
        }

        public async Task<User> GetAdmin(CancellationToken cancellationToken)
        {
            return await _OfferRepository.GetAdmin(cancellationToken);
        }

        public async Task<List<OfferDto>> GetAllOffer(int OrderId, CancellationToken cancellationToken)
        {
           return await _OfferRepository.GetAllOffer(OrderId, cancellationToken);
        }

        public async Task<List<OfferDto>> GetAllOfferAllOrder(CancellationToken cancellationToken)
        {
            return await _OfferRepository.GetAllOfferAllOrder(cancellationToken);
        }

        public async Task<Customer> GetCustomer(int id, CancellationToken cancellationToken)
        {
            return await _OfferRepository.GetCustomer(id, cancellationToken);   
        }

        public async Task<Expert> GetExpert(int id, CancellationToken cancellationToken)
        {
            return await _OfferRepository.GetExpert(id, cancellationToken);
        }

        public async Task<Offer> GetOfferById(int id, CancellationToken cancellationToken)
        {
            return await _OfferRepository.GetOfferById(id, cancellationToken);
        }

        public async Task RejectedOffer(int id, CancellationToken cToken)
        {
            await _OfferRepository.RejectedOffer(id, cToken);
        }

        public async Task UpdateOffer(Offer offer, CancellationToken cancellationToken)
        {
             await _OfferRepository.UpdateOffer(offer, cancellationToken);
        }

        public async Task UpdateUserBalance(int userId, int newBalance, CancellationToken cancellationToken)
        {
           await _OfferRepository.UpdateUserBalance(userId, newBalance, cancellationToken);
        }
    }
}
