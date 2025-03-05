

using SunService.Domain.Core.SunServices.UserS.Entities;

namespace SunService.Domain.Core.SunServices.HService.Entities
{
    public class Offer
    {
        #region Properties
        public int Id { get; set; }
        public int ExpertId { get; set; }
        public int PriceOffer { get; set; }
        public DateTime OfferDate {get;set; }
        public DateTime CompletionDate {get;set; }
        public string Description { get; set; }
        public bool? StateOffer { get; set; } 
        public int OrderId { get; set; }

        #endregion
        #region NavigationProperties
        public Order Order { get; set; }
        public Expert Expert { get; set; }
        #endregion
    }
}
