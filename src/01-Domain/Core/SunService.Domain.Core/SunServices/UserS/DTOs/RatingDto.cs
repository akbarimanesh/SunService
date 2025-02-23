using SunService.Domain.Core.SunServices.UserS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.DTOs
{
    public class RatingDto
    {
        public int Id { get; set; }
        public string ExpertFullName { get; set; }
        public string CustomerFullName { get; set; }
        public string HomeServiceTitle { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public StatuseRating Status { get; set; } = StatuseRating.apending;
    }
}
