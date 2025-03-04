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
        public async Task<IActionResult> Index(int page = 1, CancellationToken cToken = default)
        {
            int pageSize = 8;
            var allCategories = await _categoryAppServices.GetAllCategories(cToken);

            var pagedCategories = allCategories
                .Skip((page - 1) * pageSize) 
                .Take(pageSize) 
                .ToList();

            int totalItems = allCategories.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;

            return View(pagedCategories);
        }

    }
}
