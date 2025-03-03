

using SunService.Domain.Core.SunServices.HService.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunService.Domain.Core.SunServices.UserS.Entities
{
    public class Expert:User
    {
        #region Properties

       

        public string? Biography { get; set; }

        #endregion

        #region NavigationProperties
        public List<Order>? Orders { get; set; }
        public List<Offer>? Offers { get; set; }
        public List<ExpertService> ExpertServices { get; set; }
        public List<Rating>? Ratings { get; set; }
     
       
        #endregion
    }
}
