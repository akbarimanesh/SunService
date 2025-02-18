using Microsoft.AspNetCore.Mvc;

namespace SunService.EndPoints.Mvc.Task.Areas.Account.Controllers
{
    public class AccessDeniedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
