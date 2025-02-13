

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SunService.Domain.Core.SunServices.HService.Enums
{
    public enum OrderHomeServiceStatusEnum
    {
        [Display(Name = "در انتظار پیشنهاد متخصص")]
        OfferExpert = 1,
        [Display(Name = "در انتظار انتخاب متخصص")]
        ChoiceExpert = 2,
        [Display(Name = "در انتظار آمدن متخصص به محل شما")]
        ExpetToCome = 3,
        [Display(Name = "اتمام کار")]
        FinishingWork = 4
       
       
    }
}
