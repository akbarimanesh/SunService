using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.AppServices.SunServices.UserS;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.Entities;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class RatingsController : Controller
    { 
        private readonly IRatingAppServices _ratingAppServices;
        private readonly UserManager<User> _userManager;
        private readonly IUserSAppServices _UserSAppServices;
        public RatingsController(IRatingAppServices ratingAppServices, UserManager<User> userManager, IUserSAppServices userSAppServices)
        {
            _ratingAppServices = ratingAppServices;
            _userManager = userManager;
            _UserSAppServices = userSAppServices;
        }
        [HttpGet]
        public async Task< IActionResult> Index(CancellationToken cToken)
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

            
            TempData["Menu-Ratings"] = "current";
            var ratings= await _ratingAppServices.GetAllRating(cToken);
            return View(ratings);
        }
        [HttpGet]
        public async Task<IActionResult> Confirmation(int id, CancellationToken cToken)
        {

            var result = await _ratingAppServices.Confirmation(id, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;
            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;
            }

            return RedirectToAction("Index", "Ratings");
        }
        [HttpGet]
        public async Task<IActionResult> Rejected(int id, CancellationToken cToken)
        {

            var result = await _ratingAppServices.Rejected(id, cToken);
            if (result.IsSuccess)
            {

                ViewBag.SuccessMessage = result.IsMessage;

            }
            else
            {
                ViewBag.ErrorMessage = result.IsMessage;

            }

            return RedirectToAction("Index", "Ratings");
        }

    }
}
