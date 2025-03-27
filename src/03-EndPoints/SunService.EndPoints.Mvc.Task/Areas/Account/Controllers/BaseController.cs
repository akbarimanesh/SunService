using Microsoft.AspNetCore.Mvc;
using SunService.Domain.Core.SunServices.HService.AppServices;

namespace SunService.EndPoints.Mvc.Task.Areas.Account.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICategoryAppServices _categoryAppServices;

        public BaseController(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }

        public async Task<IActionResult> SetCategories(CancellationToken cToken )
        {
            var categories = await _categoryAppServices.GetAllCategories(cToken);
            ViewData["Categories"] = categories;
            return View();
        }
    }
}
