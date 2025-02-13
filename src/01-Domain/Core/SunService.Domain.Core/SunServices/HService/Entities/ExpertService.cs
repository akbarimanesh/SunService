

using SunService.Domain.Core.SunServices.UserS.Entities;

namespace SunService.Domain.Core.SunServices.HService.Entities
{
    public class ExpertService
    {
        #region Properties
        public int ExpertId { get; set; }
        public int HomeServiceId { get; set; }
        public double AverageRating => Ratings.Any() ? Ratings.Average(r => r.Score) : 0;
        #endregion

        #region NavigationProperties
        public List<Rating>? Ratings { get; set; }
        public Expert Expert { get; set; }
        public HomeService HomeService { get; set; }
        #endregion
    }
}
