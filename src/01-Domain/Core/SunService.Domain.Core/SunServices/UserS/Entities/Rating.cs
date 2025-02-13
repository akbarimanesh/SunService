

using SunService.Domain.Core.SunServices.HService.Entities;

namespace SunService.Domain.Core.SunServices.UserS.Entities
{
    public class Rating
    {
        #region Properties
        public int Id { get; set; }
        public int ExpertId { get; set; } 
        public int CustomerId { get; set; } 
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int HomeServiceId { get; set; }

        #endregion

        #region NavigationProperties
        public HomeService HomeService { get; set; }
        public Expert Expert { get; set; }
        public Customer Customer { get; set; }
        #endregion
    }
}
