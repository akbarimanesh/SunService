

using SunService.Domain.Core.SunServices.UserS.Entities;

namespace SunService.Domain.Core.SunServices.HService.Entities
{
    public class HomeService
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int BasePrice { get; set; }
        public int NumberVisits { get; set; }
        public string? ImagePath { get; set; }
        public int SubCategoryId { get; set; }

        #endregion

        #region NavigationProperties
        public List<ExpertService> ExpertServices { get; set; }
        public List<Order> orders { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<Rating>? Ratings { get; set; }
        #endregion

    }
}
