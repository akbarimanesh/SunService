using System.ComponentModel.DataAnnotations;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Models
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان زیر دسته بندی الزامی است")]
        public string Title { get; set; }
        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
