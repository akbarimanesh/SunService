using Microsoft.AspNetCore.Mvc.Rendering;
using SunService.Domain.Core.SunServices.UserS.Enums;
using System.ComponentModel.DataAnnotations;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Models
{
    public class UpdateViewModel
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
        public IFormFile? ProfileImgFile { get; set; }

        public bool Statuse { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک شهر انتخاب کنید.")]

            public int cityId { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک نقش معتبر انتخاب کنید.")]
            public int RoleId { get; set; }
           public RoleEnum Role {  get; set; }
            public List<SelectListItem> Roles { get; set; } = new();
        
    }
}
