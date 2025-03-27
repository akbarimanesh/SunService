using Microsoft.AspNetCore.Mvc;

using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.EndPoints.Mvc.Task.Areas.Account.Controllers;
using SunService.EndPoints.Mvc.Task.Models;
using System.Diagnostics;

namespace SunService.EndPoints.Mvc.Task.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeServiceAppServices _homeServiceAppServices;
        private readonly IRatingAppServices _ratingAppServices;
        private readonly ICategoryAppServices _categoryAppServices;
        public HomeController(
          ILogger<HomeController> logger,
          IHomeServiceAppServices homeServiceAppServices,
          IRatingAppServices ratingAppServices,
          ICategoryAppServices categoryAppServices
      ) : base(categoryAppServices) 
        {
            _logger = logger;
            _homeServiceAppServices = homeServiceAppServices;
            _ratingAppServices = ratingAppServices;
        }
        public async Task<IActionResult> Index(int page = 1, CancellationToken cToken = default)
        {
            await SetCategories(cToken);
            int pageSize = 4; 

            //var categories = await _categoryAppServices.GetAllCategories(cToken);
            var homeservices = await _homeServiceAppServices.GetAllHomeService(cToken);
            var ratings = await _ratingAppServices.GetAllRating(cToken);
           

            var pagedHomeServices = homeservices
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var categories1 = (List<CategoryDto>)ViewData["Categories"];
            var viewModel = new HomePageViewModel
            {
                homeServiceDtos = pagedHomeServices,
                categoryDtos = categories1,
                ratingDtos = ratings,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)homeservices.Count() / pageSize),
                Menu = new MenuViewModel
                {
                    CategoryDtos = categories1
                },
                SubCategories = categories1.SelectMany(c => c.SubCategories).ToList()
            };

            return View(viewModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
