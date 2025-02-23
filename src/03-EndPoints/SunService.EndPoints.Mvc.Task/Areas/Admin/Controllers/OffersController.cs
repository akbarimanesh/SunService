using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Services;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class OffersController : Controller
    {
        private readonly IOfferAppServices _offerAppServices;
        private readonly IorderAppServices _orderAppServices;

        public OffersController(IOfferAppServices offerAppServices, IorderAppServices orderAppServices)
        {
            _offerAppServices = offerAppServices;
            _orderAppServices = orderAppServices;
        }

        public async Task<IActionResult> Index(int id,CancellationToken cToken)
        {
            var order = await _orderAppServices.GetorderById(id, cToken);
            var offers = await _offerAppServices.GetAllOffer(order.Id, cToken);
            return View(offers);
        }
    }
}
