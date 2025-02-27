using Microsoft.AspNetCore.Mvc;

namespace SunService.EndPoints.Mvc.Task.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
