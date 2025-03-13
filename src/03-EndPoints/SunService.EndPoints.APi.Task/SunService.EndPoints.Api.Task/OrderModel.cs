using SunService.Domain.Core.SunServices.HService.Enums;

namespace SunService.EndPoints.Api.Task
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string? CustomerFullName { get; set; }
        public string? HomeServiceTitle { get; set; }
        public string CreateAt { get; set; }
        public string ImplementationDate { get; set; }
      
       

        public string? Description { get; set; }
        public OrderHomeServiceStatusEnum OrderHomeServiceStatus { get; set; }
      
        public int? CityId { get; set; }
      
        public List<string>? ImageUrls { get; set; }
    }
}
