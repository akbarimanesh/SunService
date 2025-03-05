using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.Services
{
    public interface IOfferServices
    {
        public global::System.Threading.Tasks.Task AcceptOffer(int id, CancellationToken cToken);
        public global::System.Threading.Tasks.Task RejectedOffer(int id, CancellationToken cToken);
        public Task<List<OfferDto>> GetAllOffer(int OrderId,CancellationToken cancellationToken);
        public Task<List<OfferDto>> GetAllOfferAllOrder(CancellationToken cancellationToken);
        public Task<Offer> GetOfferById(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateOffer(Offer offer, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteOffer(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateOffer(Offer offer, CancellationToken cancellationToken);
    }
}

