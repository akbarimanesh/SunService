using SunService.Domain.Core.SunServices.UserS.Data;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Services.SunServices.UserS
{
    public class RatingServices : IRatingServices
    {
        private readonly IRatingRepository _RatingRepository;
        public RatingServices(IRatingRepository ratingRepository)
        {
            _RatingRepository = ratingRepository;
        }

        public async Task Confirmation(int id, CancellationToken cToken)
        {
            await _RatingRepository.Confirmation(id, cToken);
        }

        public async Task CreateRating(SubRatingDto submitratingDto, int orderId, CancellationToken cancellationToken)
        {
            await _RatingRepository.CreateRating(submitratingDto, orderId, cancellationToken);
        }

        public async Task DeleteRating(int id, CancellationToken cancellationToken)
        {
           await _RatingRepository.DeleteRating(id, cancellationToken);
        }

        public async Task<List<RatingDto>> GetAllRating(CancellationToken cancellationToken)
        {
            return await _RatingRepository.GetAllRating(cancellationToken);
        }

        public async Task<Rating> GetRatingById(int id, CancellationToken cancellationToken)
        {
            return await _RatingRepository.GetRatingById(id, cancellationToken);
        }

        public async Task<List<RatingDto>> GetRatingsByExpertId(int expertId, int homeServiceId, CancellationToken cancellationToken)
        {
            return await _RatingRepository.GetRatingsByExpertId(expertId, homeServiceId, cancellationToken);    
        }

        public async  Task Rejected(int id, CancellationToken cToken)
        {
            await _RatingRepository.Rejected(id, cToken);
        }

        public async Task UpdateRating(Rating rating, CancellationToken cancellationToken)
        {
            await _RatingRepository.UpdateRating(rating, cancellationToken);
        }
    }
}
