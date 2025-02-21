using System.ComponentModel.DataAnnotations;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان دسته بندی الزامی است")]
        public string Title { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ProfileImgFile { get; set; }
    }
}
