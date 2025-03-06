using SunService.Domain.Core.SunServices.UserS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.DTOs
{
    public class SubRatingDto
    {
        public int Id { get; set; }
        public int ExpertId { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int HomeServiceId { get; set; }
        public StatuseRating Status { get; set; }
    }
}
