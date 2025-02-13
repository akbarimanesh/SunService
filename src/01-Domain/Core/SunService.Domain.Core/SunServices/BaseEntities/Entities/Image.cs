


using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Entities;

public class Image
{
    #region Properties
    public int Id { get; set; }
    public string Path { get; set; }
    public int OrderId { get; set; }
    
    #endregion

    #region NavigationProperties

    public Order Order { get; set; }
    
    #endregion
}