using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.DTOs
{
    public class StatisticsDataDto
    {
        public int CustomerCount { get; set; }
        public int ExpertCount { get; set; }
        public int OrderCount { get; set; }
        public int OfferCount { get; set; }
    }
}
