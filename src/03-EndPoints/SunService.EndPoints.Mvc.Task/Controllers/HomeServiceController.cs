using Microsoft.AspNetCore.Mvc;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;

namespace SunService.EndPoints.Mvc.Task.Controllers
{
    public class HomeServiceController : Controller
    {
        private readonly IHomeServiceAppServices _homeServiceAppServices;

        public HomeServiceController(IHomeServiceAppServices homeServiceAppServices)
        {
            _homeServiceAppServices = homeServiceAppServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task< IActionResult> Create(CancellationToken cancellationToken)
        {
            var homeService = new HomeServiceDto(); 
            return View(homeService);

        }
       
    }
}
