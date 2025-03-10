using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.EndPoints.Mvc.Task.Areas.Customer.Models;

namespace SunService.EndPoints.Mvc.Task.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize]
    public class ExpertController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserSAppServices _UserSAppServices;
        private readonly IHomeServiceAppServices _homeServiceAppServices;

        public ExpertController(UserManager<User> userManager, IUserSAppServices userSAppServices, IHomeServiceAppServices homeServiceAppServices)
        {
            _userManager = userManager;
            _UserSAppServices = userSAppServices;
            _homeServiceAppServices = homeServiceAppServices;
        }


        public async Task<IActionResult> Index(CancellationToken cToken)
        {
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

                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(CancellationToken cToken)
        {
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
                Selectedhomeservice = selectedHomeServices,
                Homeservices = homeservices.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title }).ToList()
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
                Selectedhomeservice = selectedHomeServices,
                Homeservices = homeservices.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title }).ToList()
            };

            ViewBag.UserProfile = user != null
                ? new UpdateViewModelUser { Id = user.Id, ImagePath = user.ImagePath ?? "~/images/Profiles/default-profile.jpg" }
                : new UpdateViewModelUser { ImagePath = "~/images/Profiles/default-profile.jpg" };

            return View(model);
        }


    }
}
