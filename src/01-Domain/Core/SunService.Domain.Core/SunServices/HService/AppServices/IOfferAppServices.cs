using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.AppServices
{
    public interface IOfferAppServices
    {
        public Task<Offer> GetOfferById(int id, CancellationToken cancellationToken);
        public Task<Result> AcceptOffer(int id, CancellationToken cToken);
        public Task<Result> RejectedOffer(int id, CancellationToken cToken);
        public Task<List<OfferDto>> GetAllOffer(int OrderId, CancellationToken cancellationToken);
        public Task<List<OfferDto>> GetAllOfferAllOrder(CancellationToken cancellationToken);
    }
}
