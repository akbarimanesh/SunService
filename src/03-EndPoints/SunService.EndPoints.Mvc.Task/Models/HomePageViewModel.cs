

using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.UserS.DTOs;


namespace SunService.EndPoints.Mvc.Task.Models
{
    public class HomePageViewModel
    {
        public List<HomeServiceDto>? homeServiceDtos { get; set; }
        public List<CategoryDto>? categoryDtos { get; set; } 
        public List<RatingDto>? ratingDtos { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public MenuViewModel Menu { get; set; } = new MenuViewModel();
        public List<SubCategoryDto> SubCategories { get; set; }
    }
}
