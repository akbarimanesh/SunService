using Microsoft.AspNetCore.Mvc.Rendering;
using SunService.Domain.Core.SunServices.UserS.Enums;
using System.ComponentModel.DataAnnotations;

namespace SunService.EndPoints.Mvc.Task.Areas.Account.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "نام کاربری الزامی است")]
        public string Username { get; set; }

        [Required(ErrorMessage = "ایمیل الزامی است")]
        public string Email { get; set; }
        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "تکرار رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیست")]
        public string ConfirmPassword { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک شهر انتخاب کنید.")]

        public int cityId { get; set; } = 0;

        [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک نقش معتبر انتخاب کنید.")]
        public int RoleId { get; set; } = 0;
        public IFormFile? ProfileImgFile { get; set; }
        public string? ImagePath { get; set; }
        public List<SelectListItem> Roles { get; set; } = new();
    }
}
