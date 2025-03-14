using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.DTOs
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public List<HomeService> HomeServices { get; set; }
        public List<SubCategoryDto> SubCategories { get; set; } = new List<SubCategoryDto>();
    }
}
