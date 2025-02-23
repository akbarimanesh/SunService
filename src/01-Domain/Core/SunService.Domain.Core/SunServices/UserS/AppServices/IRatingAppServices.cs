using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.AppServices
{
    public interface IRatingAppServices
    {
        public Task<Result> Confirmation(int id, CancellationToken cToken);
        public Task<Result> Rejected(int id, CancellationToken cToken);
        public Task<List<RatingDto>> GetAllRating(CancellationToken cancellationToken);
        public Task<Rating> GetRatingById(int id, CancellationToken cancellationToken);
    }
}
