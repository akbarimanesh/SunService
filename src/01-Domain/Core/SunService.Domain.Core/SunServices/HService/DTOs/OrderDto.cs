using Microsoft.AspNetCore.Http;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Domain.Core.SunServices.UserS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "انتخاب خدمت الزامی است.")]
        public int HomeserviceId { get; set; }
        public bool StateOrder { get; set; } = true;
        public string? CustomerFullName { get; set; }
        [Required(ErrorMessage = "تاریخ اجرا الزامی است.")]
        [DataType(DataType.Date, ErrorMessage = "فرمت تاریخ نامعتبر است.")]
        public DateTime ImplementationDate { get; set; }
        public DateTime CreateAt { get; set; }
        [Required(ErrorMessage = "زمان اجرا الزامی است.")]
        [DataType(DataType.Time, ErrorMessage = "فرمت زمان نامعتبر است.")]
        public TimeOnly ImplementationTime { get; set; }
        public OrderHomeServiceStatusEnum OrderHomeServiceStatus { get; set; } 
        public int? OfferId { get; set; }
        public List<Offer>? Offers { get; set; }
        [Required(ErrorMessage = "توضیحات الزامی است.")]
        public string? Description { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<string>? ImageUrls { get; set; }
       

    }
}
