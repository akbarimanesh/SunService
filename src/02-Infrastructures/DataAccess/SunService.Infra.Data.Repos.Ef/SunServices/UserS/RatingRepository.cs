using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Data;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Enums;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Repos.Ef.SunServices.UserS
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _appDbContext;

        public RatingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Confirmation(int id, CancellationToken cToken)
        {
            var rating = await _appDbContext.Ratings.Where(x => x.Id == id).FirstOrDefaultAsync();
            rating.Status = StatuseRating.aproved;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateRating(SubRatingDto submitratingDto, int orderId, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);
            var rating = new Rating
            {
                ExpertId = submitratingDto.ExpertId,
                CustomerId = submitratingDto.CustomerId,
                Score = submitratingDto.Score,
                Comment = submitratingDto.Comment,
                CreatedAt = DateTime.Now,
                HomeServiceId = order.HomeServiceId,
                Status = StatuseRating.apending
            };
            await _appDbContext.AddAsync(rating, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRating(int id, CancellationToken cancellationToken)
        {
            var rating = await _appDbContext.Ratings.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Ratings.Remove(rating);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<RatingDto>> GetAllRating(CancellationToken cancellationToken)
        {
            return await _appDbContext.Ratings.AsNoTracking()
                .Include(x => x.Expert)
                .Include(x => x.Expert.ExpertServices)
                    .ThenInclude(es => es.Ratings)
                .Include(x => x.Customer)
                .Select(x => new RatingDto()
                {
                    Id = x.Id,
                    HomeServiceTitle = x.HomeService.Title,
                    ExpertFullName = x.Expert.FirstName + " " + x.Expert.LastName,
                    CustomerFullName = x.Customer.FirstName + " " + x.Customer.LastName,
                    Score = x.Score,
                    Comment = x.Comment,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status,
                    ImagePathEXpert = x.Expert.ImagePath,


                    AverageRating = _appDbContext.Ratings
                        .Where(r => r.ExpertId == x.ExpertId && r.HomeServiceId == x.HomeServiceId && r.Status == StatuseRating.aproved)
                        .Average(r => (double?)r.Score) ?? 0
                })
                .ToListAsync(cancellationToken);
        }


        public async Task<Rating> GetRatingById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Ratings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<RatingDto>> GetRatingsByExpertId(int expertId, int homeServiceId, CancellationToken cancellationToken)
        {
            return await _appDbContext.Ratings
                .Where(r => r.ExpertId == expertId &&
                            r.HomeServiceId == homeServiceId &&
                            r.Status == StatuseRating.aproved) 
                .OrderByDescending(r => r.CreatedAt) 
                .Select(r => new RatingDto
                {
                    Id = r.Id,
                    ExpertFullName = r.Expert.FirstName + " " + r.Expert.LastName,
                    CustomerFullName = r.Customer.FirstName + " " + r.Customer.LastName,
                    HomeServiceTitle = r.HomeService.Title,
                    Score = r.Score,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                    AverageRating = _appDbContext.Ratings
                        .Where(x => x.ExpertId == expertId && x.HomeServiceId == homeServiceId)
                        .Average(x => x.Score) 
                })
                .ToListAsync(cancellationToken);
        }


        public async Task Rejected(int id, CancellationToken cToken)
        {
            var rating = await _appDbContext.Ratings.Where(x => x.Id == id).FirstOrDefaultAsync();
            rating.Status = StatuseRating.Rejected;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateRating(Rating rating, CancellationToken cancellationToken)
        {
            var rating1 = await _appDbContext.Ratings.FirstOrDefaultAsync(x => x.Id == rating.Id, cancellationToken);
            rating1.Id = rating.Id;
            rating1.ExpertId = rating.ExpertId;
            rating1.CustomerId = rating.CustomerId;
            rating1.Score = rating.Score;
            rating1.Comment = rating.Comment;
            rating1.HomeServiceId = rating.HomeServiceId;
            rating1.Status = rating.Status;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
