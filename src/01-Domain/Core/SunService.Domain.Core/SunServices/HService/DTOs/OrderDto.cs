using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string HomeServiceTitle { get; set; }
        public string CustomerFullName { get; set; }
        public DateTime ImplementationDate { get; set; }
        public DateTime CreateAt { get; set; }
        public TimeOnly ImplementationTime { get; set; }
        public OrderHomeServiceStatusEnum OrderHomeServiceStatus { get; set; }
        public int? OfferId { get; set; }
        public List<Offer>? Offers { get; set; }
    }
}
