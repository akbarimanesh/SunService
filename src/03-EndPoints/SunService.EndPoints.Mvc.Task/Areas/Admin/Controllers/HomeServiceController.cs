using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.AppServices.SunServices.HService;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.EndPoints.Mvc.Task.Areas.Admin.Models;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class HomeServiceController : Controller
    {
        private readonly IHomeServiceAppServices _homeServiceAppServices;

        public HomeServiceController(IHomeServiceAppServices homeServiceAppServices)
        {
            _homeServiceAppServices = homeServiceAppServices;
        }

        public async Task< IActionResult> Index(CancellationToken cToken)
        {
            TempData["Menu-HomeService"]= "current";
            var homeservices = await _homeServiceAppServices.GetAllHomeService(cToken);
            return View(homeservices);
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(HomeServiceViewModel homeservice, CancellationToken cToken)
        {
            if (!ModelState.IsValid)
            {

                return View(homeservice);
            }



            var homeservice1 = new HomeServiceDto { Id = homeservice.Id, Title = homeservice.Title, Description = homeservice.Description, BasePrice = homeservice.BasePrice, ImagePath = homeservice.ImagePath, SubCategoryTitle = homeservice.SubCategoryTitle, ProfileImgFile = homeservice.ProfileImgFile,NumberVisits=homeservice.NumberVisits,SubCategoryId=homeservice.SubCategoryId };



            var result = await _homeServiceAppServices.CreateHomeService(homeservice1, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;
                return RedirectToAction("Index", "HomeService");

            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;

            }
            return View(homeservice);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cToken)
        {

            var result = await _homeServiceAppServices.DeleteHomeService(id, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;


            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;

            }
            return RedirectToAction("Index", "HomeService");

        }
        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cToken)
        {
            var homeservice = await _homeServiceAppServices.GetHomeServiceById(id, cToken);
            if (homeservice == null)
            {
                return NotFound();
            }
            var model = new HomeServiceViewModel()
            {
                Id =homeservice.Id,
                Title = homeservice.Title,
                ImagePath = homeservice.ImagePath,
                BasePrice = homeservice.BasePrice,
                Description= homeservice.Description,
               SubCategoryTitle=homeservice.SubCategory.Title,
               SubCategoryId=homeservice.SubCategory.Id,
                
            };
            return View(model);




        }
        [HttpPost]
        public async Task<IActionResult> Update(HomeServiceViewModel homeService, CancellationToken cToken)
        {
            if (ModelState.IsValid)
            {
                var homeservice1 = new HomeServiceDto { Id = homeService.Id, Title = homeService.Title, ImagePath = homeService.ImagePath, ProfileImgFile = homeService.ProfileImgFile,BasePrice=homeService.BasePrice,Description=homeService.Description,SubCategoryTitle=homeService.SubCategoryTitle };
                var result = await _homeServiceAppServices.UpdateHomeService(homeservice1, cToken);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.IsMessage;
                }
                else
                {
                    TempData["ErrorMessage"] = result.IsMessage;
                }
                return RedirectToAction("Index", "HomeService");
            }
            return View(homeService);


        }
    }
}
    

