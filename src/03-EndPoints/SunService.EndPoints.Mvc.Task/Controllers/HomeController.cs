using Microsoft.AspNetCore.Mvc;

using SunService.Domain.Core.SunServices.HService.AppServices;

using SunService.Domain.Core.SunServices.UserS.AppServices;

using SunService.EndPoints.Mvc.Task.Models;
using System.Diagnostics;

namespace SunService.EndPoints.Mvc.Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeServiceAppServices _homeServiceAppServices;
        private readonly IRatingAppServices _ratingAppServices;
        private readonly ICategoryAppServices _categoryAppServices;
        public HomeController(ILogger<HomeController> logger, IHomeServiceAppServices homeServiceAppServices, IRatingAppServices ratingAppServices, ICategoryAppServices categoryAppServices)
        {
            _logger = logger;
            _homeServiceAppServices = homeServiceAppServices;
            _ratingAppServices = ratingAppServices;
            _categoryAppServices = categoryAppServices;
        }

        public async Task<IActionResult> Index(int page = 1, CancellationToken cToken = default)
        {
            int pageSize = 4; 

            var categories = await _categoryAppServices.GetAllCategories(cToken);
            var homeservices = await _homeServiceAppServices.GetAllHomeService(cToken);
            var ratings = await _ratingAppServices.GetAllRating(cToken);

           
            var pagedHomeServices = homeservices
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new HomePageViewModel
            {
                homeServiceDtos = pagedHomeServices,
                categoryDtos = categories,
                ratingDtos = ratings,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)homeservices.Count() / pageSize)
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
