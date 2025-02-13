
namespace SunService.Domain.Core.SunServices.HService.Entities
{
    public class Category
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImagePath { get; set; }

        #endregion

        #region NavigationProperties
        public List<SubCategory> SubCategories { get; set; }
        
        #endregion
    }
}
