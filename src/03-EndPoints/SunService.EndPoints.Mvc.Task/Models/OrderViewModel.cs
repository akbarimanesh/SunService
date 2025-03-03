using SunService.Domain.Core.SunServices.HService.DTOs;

namespace SunService.EndPoints.Mvc.Task.Models
{
    public class OrderViewModel
    {
        public List<HomeServiceDto>? HomeServices { get; set; }
        public OrderDto orderDto { get; set; }
    }
}
