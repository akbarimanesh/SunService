using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
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
using SunService.EndPoints.Mvc.Task.Areas.Admin.Models;
using SunService.EndPoints.Mvc.Task.Areas.Customer.Models;
using SunService.EndPoints.Mvc.Task.Models;
using System.Security.Claims;
using System.Threading;

namespace SunService.EndPoints.Mvc.Task.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly IorderAppServices _orderAppServices;
        private readonly IOfferAppServices _offerAppServices;
        private readonly UserManager<User> _userManager;
        private readonly IUserSAppServices _UserSAppServices;
        private readonly IRatingAppServices _ratingAppServices;
        private readonly IHomeServiceAppServices _homeServiceAppServices;
        private readonly ICategoryAppServices _categoryAppServices;
        public CustomerController(
             UserManager<User> userManager,
             IUserSAppServices UserSAppServices,
             IHomeServiceAppServices homeServiceAppServices,
             IorderAppServices orderAppServices,
             IOfferAppServices offerAppServices,
             IRatingAppServices ratingAppServices,
        ICategoryAppServices categoryAppServices
        ) : base(categoryAppServices)
        {
            _userManager = userManager;
            _UserSAppServices = UserSAppServices;
            _orderAppServices = orderAppServices;
            _offerAppServices = offerAppServices;
            _homeServiceAppServices = homeServiceAppServices;
            _ratingAppServices = ratingAppServices;
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
                UserName = user.UserName,
                RoleId = user.RoleId,
                Status = user.Status,
                Mobile = user.Mobile,
                CardNumber = user.CardNumber,
                ShabaNumber = user.ShabaNumber,
                Balance = user.Balance ?? 0,

                cityId = user.CityId,

                ImagePath = user.ImagePath,
                ProfileImgFile = user.ProfileImgFile,

            };



            ViewBag.UserProfile = user != null
                ? new UpdateViewModelUser { Id = user.Id, ImagePath = user.ImagePath ?? "~/images/Profiles/default-profile.jpg" }
                : new UpdateViewModelUser { ImagePath = "~/images/Profiles/default-profile.jpg" };

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModelUser user, CancellationToken cToken)
        {
            if (ModelState.IsValid)
            {

                var user1 = new UserDto { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, CityId = user.cityId, Mobile = user.Mobile, Address = user.Address, ProfileImgFile = user.ProfileImgFile, Balance = user.Balance, CardNumber = user.CardNumber, ShabaNumber = user.ShabaNumber, UserName = user.UserName, RoleId = user.RoleId ?? 0, Status = user.Status ?? false };
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
        public async Task<IActionResult> Order(CancellationToken cancellationToken)
        {
            await SetCategories(cancellationToken);
            var userId = _userManager.GetUserId(User);
            var id = int.Parse(userId);
            var orders = await _orderAppServices.GetAllOrderUser(id, cancellationToken);

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
        public async Task<IActionResult> Offer(int id, CancellationToken cToken)
        {
            await SetCategories(cToken);
            var order = await _orderAppServices.GetorderById(id, cToken);
            var offers = await _offerAppServices.GetAllOffer(order.Id, cToken);
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

            return View(offers);
        }
        [HttpGet]
        public async Task<IActionResult> AcceptOffer(int id, CancellationToken cToken)
        {
            await SetCategories(cToken);
            var result = await _offerAppServices.AcceptOffer(id, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;
            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;
            }
            var offer = await _offerAppServices.GetOfferById(id, cToken);
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

            return RedirectToAction("Offer", "Customer", new { id = offer.OrderId });

        }
        [HttpGet]
        public async Task<IActionResult> RejectOffer(int id, CancellationToken cToken)
        {
            await SetCategories(cToken);
            var result = await _offerAppServices.RejectedOffer(id, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;
            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;
            }
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

            return RedirectToAction("Offer", "Customer");
        }
        [HttpGet]
        public async Task<IActionResult> Payment(int offerId, CancellationToken cToken)
        {
            await SetCategories(cToken);
            var result = await _offerAppServices.UpdateBalances(offerId, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;
            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;
            }

            var offer = await _offerAppServices.GetOfferById(offerId, cToken);
            return RedirectToAction("Score", "Customer", new { orderId = offer.OrderId });
        }

        [HttpGet]
        public async Task<IActionResult> Score(int orderId, CancellationToken cToken)
        {
            await SetCategories(cToken);
            var order = await _orderAppServices.GetorderById(orderId, cToken);
            if (order == null) return NotFound();

            var model = new SubRatingDto
            {
                ExpertId = order.ExpertId ?? 0,
                CustomerId = order.CustomerId,
                OrderId = orderId
            };

            ViewBag.OrderId = orderId;
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
        public async Task<IActionResult> Score(SubRatingDto ratingDto, CancellationToken cToken)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "لطفاً تمام فیلدها را پر کنید.";
                return RedirectToAction("Score", new { orderId = ratingDto.OrderId });
            }

            var result = await _ratingAppServices.CreateRating(ratingDto, ratingDto.OrderId, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;
            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;
            }


            return RedirectToAction("Order", "Customer");


        }
        [HttpGet]
        public async Task<IActionResult> ProfileExpert(int id, int homeserviceId, CancellationToken cToken)
        {
            await SetCategories(cToken);

            
            var expertUser = await _UserSAppServices.GetById(id, cToken);
            var expertDetails = await _UserSAppServices.GetExpert(id, cToken);

           
            var homeservices = await _homeServiceAppServices.GetAllHomeService(cToken);
            var selectedHomeServices = await _UserSAppServices.GetHomeServicesExpert(id, cToken);
            var ratings = await _ratingAppServices.GetRatingsByExpertId(id, homeserviceId, cToken);

           
            var model = new UpdateViewModelUser
            {
                Id = expertUser.Id,
                FirstName = expertUser.FirstName,
                LastName = expertUser.LastName,
                Address = expertUser.Address,
                Email = expertUser.Email,
                UserName = expertUser.UserName,
                RoleId = expertUser.RoleId,
                Mobile = expertUser.Mobile,
                Status = expertUser.Status,
                CardNumber = expertUser.CardNumber,
                ShabaNumber = expertUser.ShabaNumber,
                Balance = expertUser.Balance ?? 0,
                cityId = expertUser.CityId,
                ImagePath = expertUser.ImagePath ?? "~/images/Profiles/default-profile.jpg",
                ProfileImgFile = expertUser.ProfileImgFile,
                Selectedhomeservice = selectedHomeServices,
                Biography = expertDetails?.Biography,
                Homeservices = homeservices.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Title
                }).ToList(),
                Ratings = ratings ?? new List<RatingDto>()
            };

            return View(model);



        }
    }
}
