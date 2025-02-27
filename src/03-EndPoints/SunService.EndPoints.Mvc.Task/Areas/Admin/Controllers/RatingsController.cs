using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.Core.SunServices.UserS.AppServices;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class RatingsController : Controller
    { 
        private readonly IRatingAppServices _ratingAppServices;

        public RatingsController(IRatingAppServices ratingAppServices)
        {
            _ratingAppServices = ratingAppServices;
        }
        [HttpGet]
        public async Task< IActionResult> Index(CancellationToken cToken)
        {
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
