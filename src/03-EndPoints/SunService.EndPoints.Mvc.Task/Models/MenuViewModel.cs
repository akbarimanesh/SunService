using SunService.Domain.Core.SunServices.HService.DTOs;

namespace SunService.EndPoints.Mvc.Task.Models
{
    public class MenuViewModel
    {
        public List<CategoryDto> CategoryDtos { get; set; } = new List<CategoryDto>();
    }
}
