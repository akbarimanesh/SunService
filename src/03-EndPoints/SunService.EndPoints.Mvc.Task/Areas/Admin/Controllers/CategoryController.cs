using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.EndPoints.Mvc.Task.Areas.Admin.Models;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryAppServices _categoryAppServices;

        public CategoryController(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }

        public async Task<IActionResult> Index(CancellationToken cToken)
        {
            var categories = await _categoryAppServices.GetAllCategories(cToken);
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel category, CancellationToken cToken)
        {
            if (!ModelState.IsValid)
            {
                
                return View(category);
            }



            var category1= new CategoryDto {Id=category.Id,Title=category.Title,ImagePath=category.ImagePath,ProfileImgFile=category.ProfileImgFile };
           

            var result = await _categoryAppServices.CreateCategory(category1, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;
                return RedirectToAction("Index", "Category");

            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;

            }
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cToken)
        {

            var result = await _categoryAppServices.DeleteCategory(id, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;


            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;

            }
            return RedirectToAction("Index", "Category");

        }
        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cToken)
        {
            var category = await _categoryAppServices.GetCategoryById(id, cToken);
            if (category == null)
            {
                return NotFound();
            }
            var model = new CategoryViewModel()
            {
                Id=category.Id,
                Title=category.Title,
                ImagePath=category.ImagePath
            };
            return View(model);

           


        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryViewModel category, CancellationToken cToken)
        {
            if (ModelState.IsValid)
            {
                var category1 = new CategoryDto { Id = category.Id, Title = category.Title, ImagePath = category.ImagePath,ProfileImgFile=category.ProfileImgFile };
                var result = await _categoryAppServices.UpdateCategory(category1, cToken);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.IsMessage;
                }
                else
                {
                    TempData["ErrorMessage"] = result.IsMessage;
                }
                return RedirectToAction("Index", "Category");
            }
            return View(category);


        }
    }
}
