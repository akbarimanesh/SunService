using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.Enums
{
    public enum StatuseRating
    {
        [Display(Name = "معلق")]
        apending = 1,
        [Display(Name = "تایید شده")]
        aproved = 2,
        [Display(Name = " رد شده")]
        Rejected = 3
    }
}
