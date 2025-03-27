using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.DTOs
{
    public class StatisticsDataExpertDto
    {
        public int BalanceCount { get; set; }
        public int OrderCount { get; set; }
        public int SkillCount { get; set; }
        public int ServiceCount { get; set; }
    }
}
