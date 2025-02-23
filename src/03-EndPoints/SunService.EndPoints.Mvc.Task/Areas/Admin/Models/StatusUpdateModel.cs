using Microsoft.AspNetCore.Mvc.Rendering;
using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Domain.Core.SunServices.UserS.Enums;
using System.ComponentModel.DataAnnotations;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Models
{
    public class StatusUpdateModel
    {
        public int OrderId { get; set; }
        [Required(ErrorMessage = "انتخاب وضعیت الزامی است")]
        public int Status { get; set; }
        public OrderHomeServiceStatusEnum statusEnum { get; set; }
        public List<SelectListItem> Statuses { get; set; } = new();
    }
}
