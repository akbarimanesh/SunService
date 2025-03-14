using Microsoft.AspNetCore.Mvc;
using SunService.Domain.Core.SunServices.HService.AppServices;

namespace SunService.EndPoints.Mvc.Task.Controllers
{
    public class SubcategoriesController : Controller
    {
        private readonly ISubCategoryAppServices _subCategoryAppServices;

        public SubcategoriesController(ISubCategoryAppServices subCategoryAppServices)
        {
            _subCategoryAppServices = subCategoryAppServices;
        }

        public async Task<IActionResult> Index(int categoryId, CancellationToken cToken)
        {
            if (categoryId == 0)
            {
                TempData["ErrorMessage"] = "شناسه دسته‌بندی معتبر نیست.";
                return RedirectToAction("Index", "Categories");
            }

            var subCategories = await _subCategoryAppServices.GetSubCategoriesByCategoryId(categoryId, cToken);
            var categoryName = subCategories.FirstOrDefault()?.CategoryName;
            ViewData["CategoryName"] = categoryName;
            if (subCategories == null || !subCategories.Any())
            {
                TempData["ErrorMessage"] = "زیردسته‌ای برای این دسته‌بندی یافت نشد.";
                return RedirectToAction("Index", "Categories");
            }

            return View(subCategories);
        }
    }
}

