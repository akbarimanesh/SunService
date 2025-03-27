using Microsoft.AspNetCore.Mvc;
using SunService.Domain.AppServices.SunServices.UserS;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.EndPoints.Mvc.Task.Areas.Account.Controllers;
using SunService.EndPoints.Mvc.Task.Models;

namespace SunService.EndPoints.Mvc.Task.Controllers
{
    public class HomeServiceController : BaseController
    {
        
        private readonly ICategoryAppServices _categoryAppServices;
        private readonly IHomeServiceAppServices _homeServiceAppServices;
        public HomeServiceController(
      ICategoryAppServices categoryAppServices,
      IHomeServiceAppServices homeServiceAppServices
       ) : base(categoryAppServices)
        {

            _homeServiceAppServices= homeServiceAppServices;
        }
        public async Task<IActionResult> Index(int page = 1, CancellationToken cToken = default)
        {
            await SetCategories(cToken);
            int pageSize = 5;

            //var categories = await _categoryAppServices.GetAllCategories(cToken);
            var homeservices = await _homeServiceAppServices.GetAllHomeService(cToken);
            


            var pagedHomeServices = homeservices
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var categories1 = (List<CategoryDto>)ViewData["Categories"];
            var viewModel = new HomePageViewModel
            {
                homeServiceDtos = pagedHomeServices,
                categoryDtos = categories1,
               
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)homeservices.Count() / pageSize)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> SubCategoryHomeServices(int subCategoryId, int page = 1, CancellationToken cToken = default)
        {
            await SetCategories(cToken);
            int pageSize = 5;

           
            var homeServices = await _homeServiceAppServices.GetHomeServicesBySubCategoryId(subCategoryId, cToken);

           
            var pagedHomeServices = homeServices
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            //  var categories = await _categoryAppServices.GetAllCategories(cToken);

            var categories1 = (List<CategoryDto>)ViewData["Categories"];
            var viewModel = new HomePageViewModel
            {
                homeServiceDtos = pagedHomeServices,
                categoryDtos = categories1,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)homeServices.Count() / pageSize)
            };

            return View("Index", viewModel);
        }

    }
}
