using Microsoft.AspNetCore.Mvc;
using SunService.Domain.Core.SunServices.HService.AppServices;
<<<<<<< HEAD
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.EndPoints.Mvc.Task.Areas.Account.Controllers;
using SunService.EndPoints.Mvc.Task.Models;

namespace SunService.EndPoints.Mvc.Task.Controllers
{
    public class SubcategoriesController : BaseController
    {
        private readonly ISubCategoryAppServices _subCategoryAppServices;

        public SubcategoriesController(
            ISubCategoryAppServices subCategoryAppServices,
            ICategoryAppServices categoryAppServices) : base(categoryAppServices)
        {
            _subCategoryAppServices = subCategoryAppServices;
        }
        public async Task<IActionResult> Index(int categoryId, CancellationToken cToken)
        {
            await SetCategories(cToken); 

=======

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
>>>>>>> 428ace9864b323b8f7437fedf5fea4be5a89b919
            if (categoryId == 0)
            {
                TempData["ErrorMessage"] = "شناسه دسته‌بندی معتبر نیست.";
                return RedirectToAction("Index", "Categories");
            }

            var subCategories = await _subCategoryAppServices.GetSubCategoriesByCategoryId(categoryId, cToken);
            var categoryName = subCategories.FirstOrDefault()?.CategoryName;
<<<<<<< HEAD

=======
            ViewData["CategoryName"] = categoryName;
>>>>>>> 428ace9864b323b8f7437fedf5fea4be5a89b919
            if (subCategories == null || !subCategories.Any())
            {
                TempData["ErrorMessage"] = "زیردسته‌ای برای این دسته‌بندی یافت نشد.";
                return RedirectToAction("Index", "Categories");
            }

<<<<<<< HEAD
            var allCategories = (List<CategoryDto>)ViewData["Categories"]; 

            var viewModel = new HomePageViewModel
            {
                SubCategories = subCategories,
                categoryDtos = allCategories, 
                Menu = new MenuViewModel
                {
                    CategoryDtos = allCategories 
                }
            };

            ViewData["CategoryName"] = categoryName;

            return View(viewModel);
=======
            return View(subCategories);
>>>>>>> 428ace9864b323b8f7437fedf5fea4be5a89b919
        }
    }
}

