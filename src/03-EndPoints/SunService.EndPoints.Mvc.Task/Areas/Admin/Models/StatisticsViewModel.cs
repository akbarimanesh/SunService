using SunService.Domain.Core.SunServices.HService.DTOs;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Models
{
    public class StatisticsViewModel
    {
       public  List<OrderDto>? orderDtos { get; set; }
        public  StatisticsDataDto statisticsDataDto { get; set; }
        public  List<OfferDto>? offerDtos { get; set; }
    }
}
