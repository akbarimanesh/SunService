using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.AppServices.SunServices.HService;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.EndPoints.Mvc.Task.Areas.Admin.Models;
using System.Threading;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IorderAppServices _orderAppServices;
        private readonly IOfferAppServices _offerAppServices;
        private readonly IGetStatisticsDataAppServices _getStatisticsDataAppServices;

        public HomeController(IGetStatisticsDataAppServices getStatisticsDataAppServices, IorderAppServices orderAppServices, IOfferAppServices offerAppServices)
        {
            _getStatisticsDataAppServices = getStatisticsDataAppServices;
            _orderAppServices = orderAppServices;
            _offerAppServices = offerAppServices;
        }

        public async Task< IActionResult> Index(CancellationToken cToken)
        {
          
            TempData["Menu-Dashboard"] = "current";
          var  DashboardData = await _getStatisticsDataAppServices.StatisticsDataCount(cToken);
            var orders = await _orderAppServices.GetAllOrder(cToken);
            var offers=await _offerAppServices.GetAllOfferAllOrder(cToken);
            var viewModel = new StatisticsViewModel
            {
                orderDtos = orders,
                offerDtos = offers,
                statisticsDataDto = DashboardData
            };
            return View(viewModel);
        }
    }
}
