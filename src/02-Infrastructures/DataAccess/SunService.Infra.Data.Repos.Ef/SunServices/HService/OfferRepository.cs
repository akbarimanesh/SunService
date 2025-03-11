using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Enums;
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

        public async Task AcceptOffer(int id, CancellationToken cToken)
        {
            var offer = await _appDbContext.Offers.Where(x => x.Id == id).Include(x=>x.Order).FirstOrDefaultAsync();
            offer.StateOffer = true;
            offer.Order.OrderHomeServiceStatus = OrderHomeServiceStatusEnum.ExpetToCome;
            offer.Order.OfferId = offer.Id;
            offer.Order.ExpertId = offer.ExpertId;
            var otherOffers = await _appDbContext.Offers
           .Where(o => o.OrderId == offer.OrderId && o.Id != id)
            .ToListAsync(cToken);

            foreach (var offer1 in otherOffers)
            {
                offer1.StateOffer = false; 
            }
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ChangeStatuseOrder(int orderId, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);
            order.OrderHomeServiceStatus = OrderHomeServiceStatusEnum.FinishingWork;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        
        }

        public async Task CreateOffer(OfferDto offerdto, CancellationToken cancellationToken)
        {
            var offer = new Offer()
            {

                Description = offerdto.Description,
                PriceOffer = offerdto.PriceOffer,
                
                CompletionDate = offerdto.CompletionDate,
                OfferDate = offerdto.OfferDate,
                OrderId = offerdto.OrderId,
                StateOffer = offerdto.StateOffer,
                ExpertId = offerdto.ExpertId ,
                
                
            };
            await _appDbContext.Offers.AddAsync(offer, cancellationToken);
         
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(o => o.Id == offerdto.OrderId, cancellationToken);
            if (order != null)
            {
                
                order.OrderHomeServiceStatus = OrderHomeServiceStatusEnum.ChoiceExpert;
                await _appDbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task DeleteOffer(int id, CancellationToken cancellationToken)
        {
            var offer = await _appDbContext.Offers.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Offers.Remove(offer);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

       
        public async Task<User> GetAdmin(CancellationToken cancellationToken)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(a => a.Id == 1, cancellationToken);
        }
        public async Task<List<OfferDto>> GetAllOffer(int OrderId, CancellationToken cancellationToken)
        {
            var offers = await _appDbContext.Offers
                .AsNoTracking()
                .Where(x => x.OrderId == OrderId)
                .Include(x => x.Expert) 
                .Include(x => x.Expert.ExpertServices) 
                    .ThenInclude(es => es.Ratings) 
                .Select(x => new OfferDto()
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
                    ExpertId = x.ExpertId,
                   HomeserviceId=x.Order.HomeServiceId,
                    AverageRating = x.Expert.ExpertServices
                        .Where(es => es.HomeServiceId == x.Order.HomeServiceId) 
                        .SelectMany(es => es.Ratings) 
                        .Where(r => r.Status == StatuseRating.aproved) 
                        .Average(r => (double?)r.Score) ?? 0 
                })
                .ToListAsync(cancellationToken);

            return offers;
        }

        public async Task<List<OfferDto>> GetAllOfferAllOrder(CancellationToken cancellationToken)
        {
            return await _appDbContext.Offers.AsNoTracking().Select(x => new OfferDto()
            {
                Id = x.Id,
                HomeServiceTitle = x.Order.HomeService.Title,
                ExpertFullName = x.Expert.FirstName + " " + x.Expert.LastName,
                ExpertId=x.Id,
                PriceOffer = x.PriceOffer,
                Description = x.Description,
                OfferDate = x.OfferDate,
                OrderId = x.OrderId,
                CompletionDate = x.CompletionDate,
                StateOffer = x.StateOffer,

            }).ToListAsync(cancellationToken);
        }

        public async Task<Customer> GetCustomer(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Customers.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Expert> GetExpert(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Experts.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Offer> GetOfferById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Offers.AsNoTracking().Include(o => o.Order)
            .ThenInclude(o => o.Expert)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
       
        public async Task RejectedOffer(int id, CancellationToken cToken)
        {
            var offer = await _appDbContext.Offers.Where(x => x.Id == id).FirstOrDefaultAsync();
            offer.StateOffer = false;
            await _appDbContext.SaveChangesAsync();
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

        public async Task UpdateUserBalance(int userId, int newBalance, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            user.Balance = newBalance;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
