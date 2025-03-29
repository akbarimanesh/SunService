using SunService.Domain.Core.SunServices.UserS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.DTOs
{
    public class OfferDto
    {
        public int Id { get; set; }
       
       public string? HomeServiceTitle { get; set; }
       public int? HomeserviceId { get; set; }
        public string? ExpertFullName { get; set; }
        public DateTime CompletionDate { get; set; }
        public int PriceOffer { get; set; }
        public DateTime OfferDate { get; set; }
        [Required(ErrorMessage = "توضیحات الزامی است.")]
        public string Description { get; set; }
        public int OrderId { get; set; }
        public int ExpertId { get; set; }
        public int? BasePrice { get; set; }
        public bool? StateOffer { get; set; }
        public double AverageRating { get; set; } 
    }
}
