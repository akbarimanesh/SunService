using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Models
{
    public class CreateViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        [Required(ErrorMessage = "نام کاربری الزامی است")]
        public string Username { get; set; }

        [Required(ErrorMessage = "ایمیل الزامی است")]
        public string Email { get; set; }
        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public IFormFile? ProfileImgFile { get; set; }
        public string? ImagePath { get; set; }
        public bool Statuse { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک شهر انتخاب کنید.")]

        public int cityId { get; set; } = 0;

        [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک نقش معتبر انتخاب کنید.")]
        public int RoleId { get; set; } = 0;
        public List<SelectListItem> Roles { get; set; } = new();
    }
}

