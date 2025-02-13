using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Data;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
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

        public async Task CreateRating(Rating rating, CancellationToken cancellationToken)
        {
            await _appDbContext.Ratings.AddAsync(rating, cancellationToken);
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
            return await _appDbContext.Ratings.AsNoTracking().Select(x => new RatingDto()
            {
                Id = x.Id,
                HomeServiceTitle = x.HomeService.Title,
                ExpertFullName = x.Expert.FirstName + " " + x.Expert.LastName,
                CustomerFullName = x.Customer.FirstName + " " + x.Customer,
                Score = x.Score,
                Comment = x.Comment,
                CreatedAt = x.CreatedAt


            }).ToListAsync(cancellationToken);
        }

        public async Task<Rating> GetRatingById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Ratings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
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
            
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
