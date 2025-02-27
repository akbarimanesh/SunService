

using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Domain.Core.SunServices.UserS.Entities;

namespace SunService.Domain.Core.SunServices.HService.Entities
{
    public class Order
    {
        #region Properties
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ImplementationDate { get; set; }
        public DateTime CreateAt { get; set; }
        public TimeOnly ImplementationTime {  get; set; }
        public int CityId { get; set; }
        public bool StateOrder{ get; set; } = true;
        public OrderHomeServiceStatusEnum OrderHomeServiceStatus { get; set; }
        public int? OfferId { get; set; }
        public int CustomerId { get; set; }
        public int? ExpertId { get; set; }
        public int HomeServiceId { get; set; }
        #endregion

        #region NavigationProperties
        public List<Offer>? Offers { get; set; }
        public Customer Customer { get; set; }
        public HomeService HomeService { get; set; }
        public City City { get; set; }
        public List<Image> Images { get; set; }
        public Expert? Expert { get; set; }
        #endregion
    }
}
