using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using SunService.Domain.Core.SunServices.UserS.Enums;
using System.ComponentModel.DataAnnotations;

namespace SunService.EndPoints.Mvc.Task.Areas.Customer.Models
{
    public class UpdateViewModelUser
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
      
        [Required(ErrorMessage = "شماره کارت الزامی است.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "شماره کارت باید دقیقا 16 رقم باشد و فقط شامل اعداد باشد.")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "شماره شبا الزامی است.")]
        [RegularExpression(@"^IR\d{24}$", ErrorMessage = "شماره شبا باید با IR شروع شده و دقیقا 24 رقم بعدی باشد.")]
        public string ShabaNumber { get; set; }
        [Required(ErrorMessage = "موجودی الزامی است.")]
        [Range(0, int.MaxValue, ErrorMessage = "موجودی باید یک عدد صحیح غیر منفی باشد.")]
        public int Balance { get; set; }

        [Required(ErrorMessage = "ایمیل الزامی است")]
        public string Email { get; set; }
        public IFormFile? ProfileImgFile { get; set; }
      
        public bool? Status { get; set; }
       
        public string? UserName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک شهر انتخاب کنید.")]

        public int cityId { get; set; }
     
        public int? RoleId { get; set; }
        public string? ImagePath { get; set; }


    }
}
