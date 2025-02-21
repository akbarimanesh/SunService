using System.ComponentModel.DataAnnotations;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Models
{
    public class HomeServiceViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان دسته بندی الزامی است")]
        public string Title { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ProfileImgFile { get; set; }
        [Required(ErrorMessage = "توضیحات الزامی است")]
        public string Description { get; set; }
        [Required(ErrorMessage = "قیمت پایه الزامی است")]
        [Range(1, int.MaxValue, ErrorMessage = "قیمت باید عددی بزرگتر از صفر باشد")]
        [DataType(DataType.Currency)]
        public int BasePrice { get; set; }
        [Required(ErrorMessage = "انتخاب زیردسته الزامی است")]
        public int SubCategoryId { get; set; }

        public int? NumberVisits { get; set; }
       
        public string? SubCategoryTitle { get; set; }
    }
}
