using Microsoft.AspNetCore.Http;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Domain.Core.SunServices.UserS.Entities;
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
        public string? HomeServiceTitle { get; set; }
        public int CustomerId { get; set; }
   public Customer? Customer { get; set; }
        public int? CityId { get; set; }
     
        public int? ExpertId { get; set; }
        public int HomeserviceId { get; set; }
     
        public string? CustomerFullName { get; set; }
        public DateTime ImplementationDate { get; set; }
        public DateTime CreateAt { get; set; }
        public TimeOnly ImplementationTime { get; set; }
        public OrderHomeServiceStatusEnum OrderHomeServiceStatus { get; set; } 
        public int? OfferId { get; set; }
        public List<Offer>? Offers { get; set; }
        public string? Description { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<string>? ImageUrls { get; set; }
       

    }
}
