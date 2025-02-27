using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.Core.SunServices.HService.AppServices;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IGetStatisticsDataAppServices _getStatisticsDataAppServices;

        public HomeController(IGetStatisticsDataAppServices getStatisticsDataAppServices)
        {
            _getStatisticsDataAppServices = getStatisticsDataAppServices;
        }

        public async Task< IActionResult> Index(CancellationToken cToken)
        {
          
            TempData["Menu-Dashboard"] = "current";
          var  DashboardData = await _getStatisticsDataAppServices.StatisticsDataCount(cToken);
            return View(DashboardData);
        }
    }
}
