using System.ComponentModel.DataAnnotations;

namespace SunService.EndPoints.Api.Task
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "نام کاربری الزامی است.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "ایمیل الزامی است.")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل نامعتبر است.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "تکرار رمز عبور الزامی است.")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار آن باید یکسان باشند.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "شهر الزامی است.")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "نقش الزامی است.")]
        public int RoleId { get; set; }
    }
}
