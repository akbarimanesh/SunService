

namespace SunService.Domain.Core.SunServices.HService.Entities
{
    public class SubCategory
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }

        public int CategoryId { get; set; }

        #endregion

        #region NavigationProperties
        public List<HomeService> HomeServices { get; set; }
        public Category Category { get; set; }
        
        #endregion

    }
}
