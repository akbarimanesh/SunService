using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using SunService.Domain.AppServices.SunServices.HService;
using SunService.Domain.AppServices.SunServices.UserS;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Enums;
using SunService.EndPoints.Mvc.Task.Areas.Account.Controllers;
using SunService.EndPoints.Mvc.Task.Areas.Customer.Models;
using SunService.EndPoints.Mvc.Task.Models;
using System;
using System.Threading;

namespace SunService.EndPoints.Mvc.Task.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize]
    public class ExpertController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserSAppServices _UserSAppServices;
        private readonly IHomeServiceAppServices _homeServiceAppServices;
        private readonly IorderAppServices _orderAppServices;
        private readonly IOfferAppServices _offerAppServices;
        private readonly ICategoryAppServices _categoryAppServices;
        public ExpertController(
            UserManager<User> userManager,
            IUserSAppServices UserSAppServices,
            IHomeServiceAppServices homeServiceAppServices,
            IorderAppServices orderAppServices,
            IOfferAppServices offerAppServices,
            ICategoryAppServices categoryAppServices
       ) : base(categoryAppServices)
        {
            _userManager= userManager;
            _UserSAppServices= UserSAppServices;
            _orderAppServices = orderAppServices;
            _offerAppServices = offerAppServices;
             _homeServiceAppServices = homeServiceAppServices;
            
        }

        public async Task<IActionResult> Index(CancellationToken cToken)
        {
            {
                await SetCategories(cToken);
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

                return View();

            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(CancellationToken cToken)
        {
            await SetCategories(cToken);
            var userId = _userManager.GetUserId(User);
            var homeservices = await _homeServiceAppServices.GetAllHomeService(cToken);

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var id = int.Parse(userId);
            var user = await _UserSAppServices.GetById(id, cToken);

            bool isExpert = User.IsInRole("Expert");

            var selectedHomeServices = new List<int>();

            if (isExpert)
            {
                var expert = await _UserSAppServices.GetExpert(id, cToken);
                selectedHomeServices = await _UserSAppServices.GetHomeServicesExpert(id, cToken);
            
           
            var model = new UpdateViewModelUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                UserName = user.UserName,
                RoleId = user.RoleId,
                Mobile = user.Mobile,
                Status = user.Status,
                CardNumber = user.CardNumber,
                ShabaNumber = user.ShabaNumber,
                Balance = user.Balance ?? 0,
                cityId = user.CityId,
                ImagePath = user.ImagePath,
                
                Biography = expert?.Biography,
                ProfileImgFile = user.ProfileImgFile,
                Selectedhomeservice = selectedHomeServices,
                Homeservices = homeservices.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title }).ToList()
            };

            ViewBag.UserProfile = user != null
                ? new UpdateViewModelUser { Id = user.Id, ImagePath = user.ImagePath ?? "~/images/Profiles/default-profile.jpg" }
                : new UpdateViewModelUser { ImagePath = "~/images/Profiles/default-profile.jpg" };

            return View(model);
        }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModelUser user, CancellationToken cToken)
        {

            if (ModelState.IsValid)
            {
                var user1 = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CityId = user.cityId,
                    Mobile = user.Mobile,
                    Address = user.Address,
                    ProfileImgFile = user.ProfileImgFile,
                    Balance = user.Balance,
                    CardNumber = user.CardNumber,
                    ShabaNumber = user.ShabaNumber,
                    UserName = user.UserName,
                    Biography = user.Biography,
                    RoleId = user.RoleId ?? 0,
                    Status = user.Status ?? false
                };

                var result = await _UserSAppServices.Update(user1, cToken);

                if (result.IsSuccess)
                {

                    if (user.Selectedhomeservice != null)
                    {
                        await _homeServiceAppServices.UpdateExpertServices(user.Id, user.Selectedhomeservice, cToken);
                    }

                    TempData["SuccessMessage"] = result.IsMessage;
                }
                else
                {
                    TempData["ErrorMessage"] = result.IsMessage;
                }

                return RedirectToAction("Update", "Expert");
            }

            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Profile(CancellationToken cToken)
        {
            await SetCategories(cToken);
            var userId = _userManager.GetUserId(User);
            var homeservices = await _homeServiceAppServices.GetAllHomeService(cToken);

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var id = int.Parse(userId);
            var user = await _UserSAppServices.GetById(id, cToken);

            bool isExpert = User.IsInRole("Expert");

            var selectedHomeServices = new List<int>();

            if (isExpert)
            {
                var expert = await _UserSAppServices.GetExpert(id, cToken);
                selectedHomeServices = await _UserSAppServices.GetHomeServicesExpert(id, cToken);
            }

            var model = new UpdateViewModelUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                UserName = user.UserName,
                RoleId = user.RoleId,
                Mobile = user.Mobile,
                Status = user.Status,
                CardNumber = user.CardNumber,
                ShabaNumber = user.ShabaNumber,
                Balance = user.Balance ?? 0,
                cityId = user.CityId,
                ImagePath = user.ImagePath,
                ProfileImgFile = user.ProfileImgFile,
                Biography=user.Biography,
                Selectedhomeservice = selectedHomeServices,
                Homeservices = homeservices.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title }).ToList()
            };

            ViewBag.UserProfile = user != null
                ? new UpdateViewModelUser { Id = user.Id, ImagePath = user.ImagePath ?? "~/images/Profiles/default-profile.jpg" }
                : new UpdateViewModelUser { ImagePath = "~/images/Profiles/default-profile.jpg" };

            return View(model);
        }
        public async Task<IActionResult> Order(CancellationToken cancellationToken)
        {
            await SetCategories(cancellationToken);
            var userId = _userManager.GetUserId(User);
            var id = int.Parse(userId);
            ViewBag.CurrentUserId = userId;
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            var orders = await _orderAppServices.GetAllOrderHomeserviceExpert(id, cancellationToken);

            var user = await _UserSAppServices.GetById(id, cancellationToken);


            ViewBag.UserProfile = user != null
                ? new UpdateViewModelUser { Id = user.Id, ImagePath = user.ImagePath ?? "~/images/Profiles/default-profile.jpg" }
                : new UpdateViewModelUser { ImagePath = "~/images/Profiles/default-profile.jpg" };

            return View(orders);
        }
        public async Task<IActionResult> Show(int Id, CancellationToken cancellationToken)
        {
            await SetCategories(cancellationToken);
            var userId = _userManager.GetUserId(User);
            var id = int.Parse(userId);
            var order = await _orderAppServices.GetorderById(Id, cancellationToken);
            var orderDto = new OrderDto
            {
                Id = order.Id,
                HomeServiceTitle = order.HomeService.Title,
                CreateAt = order.CreateAt,
                ImplementationDate = order.ImplementationDate,
                OrderHomeServiceStatus = order.OrderHomeServiceStatus,
                Description = order.Description,
                ImplementationTime = order.ImplementationTime,
                ImageUrls = order.Images?.Select(img => img.Path).ToList() ?? new List<string>()
            };

            var user = await _UserSAppServices.GetById(id, cancellationToken);


            ViewBag.UserProfile = user != null
                ? new UpdateViewModelUser { Id = user.Id, ImagePath = user.ImagePath ?? "~/images/Profiles/default-profile.jpg" }
                : new UpdateViewModelUser { ImagePath = "~/images/Profiles/default-profile.jpg" };

            return View(orderDto);
        }
        [HttpGet]
        public async Task<IActionResult> Offer(int id, CancellationToken cToken)
        {
            await SetCategories(cToken);
            if (id == 0)
            {
                TempData["ErrorMessage"] = "شناسه سفارش معتبر نیست.";
                return RedirectToAction("Orders", "Expert");
            }

            var order = await _orderAppServices.GetorderById(id, cToken);
            if (order == null)
            {
                TempData["ErrorMessage"] = "سفارش موردنظر یافت نشد.";
                return RedirectToAction("Orders", "Expert");
            }

            var model = new OfferDto
            {
                BasePrice = order.HomeService.BasePrice,
                OrderId = id 
            };



            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            var id1 = int.Parse(userId);
            var user = await _UserSAppServices.GetById(id1, cToken);


            ViewBag.UserProfile = user != null
                ? new UpdateViewModelUser { Id = user.Id, ImagePath = user.ImagePath ?? "~/images/Profiles/default-profile.jpg" }
                : new UpdateViewModelUser { ImagePath = "~/images/Profiles/default-profile.jpg" };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Offer(int id, OfferDto model, CancellationToken cToken)
        {

           
            if (!ModelState.IsValid)
            {

                return View(model);
            }
            var userId = _userManager.GetUserId(User);
            var id1 = int.Parse(userId);
            model.ExpertId = id1;
            var user = await _userManager.FindByIdAsync(userId);
            var CityId1 = user?.CityId;

            model.OrderId = id;


            DateTime persianDateTime = model.OfferDate;


            PersianDateTime persianDate = PersianDateTime.Parse(persianDateTime.ToString("yyyy/MM/dd"));


            DateTime gregorianDate = persianDate.ToDateTime();


            model.OfferDate = gregorianDate;

            DateTime persianDateTime1 = model.CompletionDate;


            PersianDateTime persianDate1 = PersianDateTime.Parse(persianDateTime1.ToString("yyyy/MM/dd"));


            DateTime gregorianDate1 = persianDate1.ToDateTime();


            model.CompletionDate = gregorianDate1;

       

            var result = await _offerAppServices.CreateOffer(model, id1, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;


            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;

            }
            return RedirectToAction("Order", "Expert");

        }
    }
}
