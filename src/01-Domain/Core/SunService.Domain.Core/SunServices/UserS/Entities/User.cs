using Microsoft.AspNetCore.Identity;
using SunService.Domain.Core.SunServices.HService.Entities;


namespace SunService.Domain.Core.SunServices.UserS.Entities
{
    public class User: IdentityUser<int>
    {
        #region Properties
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? ShabaNumber { get; set; }
        public string? CardNumber { get; set; }
        public int? Balance { get; set; }
        public string? Address { get; set; }
        public bool StatusUser { get; set; } = false;
        public int RoleId { get; set; }
        public int CityId { get; set; }
        
        public DateTime RegisterAt { get; set; }
        public string? ImagePath { get; set; }

        #endregion
        #region NavigationProperties
      
        public City City { get; set; }
        #endregion
    }
}
