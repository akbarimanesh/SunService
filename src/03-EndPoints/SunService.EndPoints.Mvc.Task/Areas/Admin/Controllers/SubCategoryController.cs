using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.AppServices.SunServices.HService;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.EndPoints.Mvc.Task.Areas.Admin.Models;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryAppServices _subcategoryAppServices;

        public SubCategoryController(ISubCategoryAppServices subcategoryAppServices)
        {
            _subcategoryAppServices = subcategoryAppServices;
        }

        
        public async Task<IActionResult> Index(CancellationToken cToken)
        {
            TempData["Menu-SubCategory"] = "current";
            var subcategories = await _subcategoryAppServices.GetAllSubCategories(cToken);
            return View(subcategories);
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(SubCategory subcategory, CancellationToken cToken)
        {
            var result = await _subcategoryAppServices.CreateSubCategory(subcategory, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;
                return RedirectToAction("Index", "SubCategory");

            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;

            }
            return View(subcategory);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cToken)
        {

            var result = await _subcategoryAppServices.DeleteSubCategory(id, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;


            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;

            }
            return RedirectToAction("Index", "SubCategory");

        }
        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cToken)
        {
            var subcategory = await _subcategoryAppServices.GetSubCategoryById(id, cToken);
            if (subcategory == null)
            {
                return NotFound();
            }

            var model = new SubCategoryViewModel()
            {
                Id = subcategory.Id,
                Title = subcategory.Title,
               CategoryName=subcategory.Category.Title,
               CategoryId=subcategory.Category.Id,
            };
            return View(model);


        }
        [HttpPost]
        public async Task<IActionResult> Update(SubCategoryViewModel subcategory, CancellationToken cToken)
        {
            if (ModelState.IsValid)
            {
                var subcategory1 = new SubCategoryDto { Id = subcategory.Id, Title = subcategory.Title, CategoryName=subcategory.CategoryName };
                var result = await _subcategoryAppServices.UpdateSubCategory(subcategory1, cToken);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.IsMessage;


                }
                else
                {
                    TempData["ErrorMessage"] = result.IsMessage;

                }
                return RedirectToAction("Index", "SubCategory");
            }

            return View(subcategory);


        }
    }
}
