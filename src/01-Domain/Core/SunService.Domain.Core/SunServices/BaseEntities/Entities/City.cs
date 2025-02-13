

using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Entities;

public class City
{
    #region Properties
    public int Id { get; set; }
    public string Title { get; set; }
    #endregion

    #region NavigationProperties
    public List<User> Users { get; set; }
    public List<Order> orders { get; set; }
    #endregion
}

