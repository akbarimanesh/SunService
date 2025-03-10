using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.AppServices.SunServices.HService;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.Entities;
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
        private readonly UserManager<User> _userManager;
        private readonly IUserSAppServices _UserSAppServices;
        public HomeController(IGetStatisticsDataAppServices getStatisticsDataAppServices, IorderAppServices orderAppServices, IOfferAppServices offerAppServices, UserManager<User> userManager, IUserSAppServices userSAppServices)
        {
            _getStatisticsDataAppServices = getStatisticsDataAppServices;
            _orderAppServices = orderAppServices;
            _offerAppServices = offerAppServices;
            _userManager = userManager;
            _UserSAppServices = userSAppServices;
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
           
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return NotFound();
                }

                var id = int.Parse(userId);
                var user = await _UserSAppServices.GetById(id, cToken);


                ViewBag.UserProfile = user != null
                    ? new UpdateViewModelUser { Id = user.Id, ImagePath = user.ImagePath ?? "~/images/Profiles/default-profile.jpg" }
                    : new UpdateViewModelUser { ImagePath = "~/images/Profiles/default-profile.jpg" };

                return View(viewModel);
            }
        }
    }
}
