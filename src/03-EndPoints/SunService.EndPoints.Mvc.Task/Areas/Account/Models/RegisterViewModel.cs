using SunService.Domain.Core.SunServices.UserS.Enums;
using System.ComponentModel.DataAnnotations;

namespace SunService.EndPoints.Mvc.Task.Areas.Account.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "نام کاربری الزامی است")]
        public string Username { get; set; }

      
        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "تکرار رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیست")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "انتخاب شهر الزامی است")]
        
        public int cityId { get; set; } 
        [Required(ErrorMessage = "انتخاب نقش الزامی است")]
        public RoleEnum Role { get; set; }
    }
}
