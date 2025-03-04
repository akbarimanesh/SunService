using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SunService.Domain.AppServices.SunServices.HService;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Enums;
using SunService.EndPoints.Mvc.Task.Areas.Admin.Models;
using SunService.EndPoints.Mvc.Task.Areas.Customer.Models;
using System.Security.Claims;
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
        private readonly IUserSAppServices _UserSAppServices;
        public CustomerController(IorderAppServices orderAppServices, IOfferAppServices offerAppServices, UserManager<User> userManager, IUserSAppServices userSAppServices)
        {
            _orderAppServices = orderAppServices;
            _offerAppServices = offerAppServices;
            _userManager = userManager;
            _UserSAppServices = userSAppServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update( CancellationToken cToken)
        {
            var userId = _userManager.GetUserId(User);
           
           
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            var id = int.Parse(userId);
            var user = await _UserSAppServices.GetById(id, cToken);
            var model = new UpdateViewModelUser()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                UserName= user.UserName,
                RoleId= user.RoleId,
                Status = user.Status,
                Mobile = user.Mobile,
                CardNumber=user.CardNumber,
                ShabaNumber=user.ShabaNumber,
                Balance = user.Balance ?? 0,
               
                cityId = user.CityId,
              
                ImagePath=user.ImagePath,
                ProfileImgFile = user.ProfileImgFile,
               
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModelUser user, CancellationToken cToken)
        {
            if (ModelState.IsValid)
            {
               
                var user1 = new UserDto { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, CityId = user.cityId,  Mobile = user.Mobile, Address = user.Address, ProfileImgFile = user.ProfileImgFile ,Balance=user.Balance,CardNumber=user.CardNumber,ShabaNumber=user.ShabaNumber,UserName=user.UserName,RoleId=user.RoleId ?? 0,Status=user.Status??false};
                var result = await _UserSAppServices.Update(user1, cToken);
                if (result.IsSuccess)
                {

                    TempData["SuccessMessage"] = result.IsMessage;


                }
                else
                {
                    TempData["ErrorMessage"] = result.IsMessage;

                }
                return RedirectToAction("Update", "Customer");
            }
            
            return View(user);


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
