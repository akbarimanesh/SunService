using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.DTOs
{
    public class HomeServiceDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int BasePrice { get; set; }
        public int NumberVisits { get; set; }
        public string? ImagePath { get; set; }
        public string SubCategoryTitle { get; set; }
    }
}
