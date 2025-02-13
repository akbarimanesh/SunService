
using SunService.Domain.Core.SunServices.HService.Entities;

namespace SunService.Domain.Core.SunServices.UserS.Entities
{
    public class Customer:User
    {
        #region Properties
      

        #endregion

        #region NavigationProperties
        public List<Rating>? Ratings { get; set; }
        public List<Order> orders { get; set; }
        #endregion


    }
}
