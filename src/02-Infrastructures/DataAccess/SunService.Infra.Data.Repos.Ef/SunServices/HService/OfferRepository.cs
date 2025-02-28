using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Repos.Ef.SunServices.HService
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AppDbContext _appDbContext;

        public OfferRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateOffer(Offer offer, CancellationToken cancellationToken)
        {
            await _appDbContext.Offers.AddAsync(offer, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteOffer(int id, CancellationToken cancellationToken)
        {
            var offer = await _appDbContext.Offers.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Offers.Remove(offer);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<OfferDto>> GetAllOffer(int OrderId,CancellationToken cancellationToken)
        {
            return await _appDbContext.Offers.AsNoTracking().Where(x=>x.OrderId==OrderId).Select(x => new OfferDto()
            {
                Id=x.Id,
                HomeServiceTitle = x.Order.HomeService.Title,
              ExpertFullName=x.Expert.FirstName +" "+x.Expert.LastName,
                PriceOffer=x.PriceOffer,
                Description=x.Description,
                OfferDate=x.OfferDate,
                OrderId=x.OrderId,
                CompletionDate=x.CompletionDate,
                StateOffer=x.StateOffer,

            }).ToListAsync(cancellationToken);
        }

        public async Task<List<OfferDto>> GetAllOfferAllOrder(CancellationToken cancellationToken)
        {
            return await _appDbContext.Offers.AsNoTracking().Select(x => new OfferDto()
            {
                Id = x.Id,
                HomeServiceTitle = x.Order.HomeService.Title,
                ExpertFullName = x.Expert.FirstName + " " + x.Expert.LastName,
                PriceOffer = x.PriceOffer,
                Description = x.Description,
                OfferDate = x.OfferDate,
                OrderId = x.OrderId,
                CompletionDate = x.CompletionDate,
                StateOffer = x.StateOffer,

            }).ToListAsync(cancellationToken);
        }

        public async Task<Offer> GetOfferById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Offers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateOffer(Offer offer, CancellationToken cancellationToken)
        {
            var offer1 = await _appDbContext.Offers.FirstOrDefaultAsync(x => x.Id == offer.Id, cancellationToken);
            offer1.Id = offer.Id;
            offer1.ExpertId= offer.ExpertId;
            offer1.PriceOffer =offer.PriceOffer;
            offer1.OfferDate = offer.OfferDate;
            offer1.Description = offer.Description;
            offer1.StateOffer = offer.StateOffer;
            offer1.OrderId= offer.OrderId;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
