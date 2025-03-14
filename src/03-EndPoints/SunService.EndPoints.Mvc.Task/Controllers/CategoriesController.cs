using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SunService.Domain.Core.SunServices.HService.AppServices;

namespace SunService.EndPoints.Mvc.Task.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryAppServices _categoryAppServices;

        public CategoriesController(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index( CancellationToken cToken = default)
        {
           
            var allCategories = await _categoryAppServices.GetAllCategories(cToken);

           

            return View(allCategories);
        }

    }
}
