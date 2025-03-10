using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.Services
{
    public interface IRatingServices
    {
        public Task<List<RatingDto>> GetRatingsByExpertId(int expertId, int homeServiceId, CancellationToken cancellationToken);
        public Task<List<RatingDto>> GetAllRating(CancellationToken cancellationToken);
        public Task<Rating> GetRatingById(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateRating(SubRatingDto submitratingDto, int orderId, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteRating(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateRating(Rating rating, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task Confirmation(int id, CancellationToken cToken);
        public global::System.Threading.Tasks.Task Rejected(int id, CancellationToken cToken);
    }
}
