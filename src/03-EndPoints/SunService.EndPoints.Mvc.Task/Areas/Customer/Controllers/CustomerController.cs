using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.AppServices.SunServices.HService;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.UserS.Entities;
using System.Threading;

namespace SunService.EndPoints.Mvc.Task.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IorderAppServices _orderAppServices;
        private readonly IOfferAppServices _offerAppServices;
        private readonly UserManager<User> _userManager;
        public CustomerController(IorderAppServices orderAppServices, IOfferAppServices offerAppServices, UserManager<User> userManager)
        {
            _orderAppServices = orderAppServices;
            _offerAppServices = offerAppServices;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        public async Task< IActionResult> Order(CancellationToken cancellationToken)
        {
            var userId = _userManager.GetUserId(User);
            var id = int.Parse(userId);
            var orders = await _orderAppServices.GetAllOrderUser(id,cancellationToken);
            return View(orders);
        }
        public async Task<IActionResult> Offer(int id, CancellationToken cToken)
        {
            var order = await _orderAppServices.GetorderById(id, cToken);
            var offers = await _offerAppServices.GetAllOffer(order.Id, cToken);
            return View(offers);
        }
    }
}
