using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.AppServices.SunServices.UserS
{
    public class RatingAppServices : IRatingAppServices
    {
        private readonly IRatingServices _ratingServices;

        public RatingAppServices(IRatingServices ratingServices)
        {
            _ratingServices = ratingServices;
        }

        public async Task<Result> Confirmation(int id, CancellationToken cToken)
        {
            await _ratingServices.Confirmation(id, cToken);
            return new Result(true, "تایید شد.");
        }

        public async Task<List<RatingDto>> GetAllRating(CancellationToken cancellationToken)
        {
            return await _ratingServices.GetAllRating(cancellationToken);
        }

        public async Task<Rating> GetRatingById(int id, CancellationToken cancellationToken)
        {
            if (await _ratingServices.GetRatingById(id, cancellationToken) != null)
            {
                return await _ratingServices.GetRatingById(id, cancellationToken);

            }
            else
                return null;
        }

        public async Task<Result> Rejected(int id, CancellationToken cToken)
        {
            await _ratingServices.Rejected(id, cToken);
            return new Result(true, "رد شد.");
        }
    }
}
