using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Enums;
using SunService.EndPoints.Mvc.Task.Areas.Account.Models;
using SunService.EndPoints.Mvc.Task.Areas.Admin.Controllers;
using SunService.EndPoints.Mvc.Task.Areas.Admin.Models;
using System.Threading.Tasks;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserSAppServices _UserSAppServices;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HomeController> _logger;


        private readonly SignInManager<User> _signInManager;


        public UsersController(IUserSAppServices userSAppServices, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _UserSAppServices = userSAppServices;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken cToken)
        {
          
            TempData["Menu-Users"] = "current";
            var users = await _UserSAppServices.GetAll(cToken);
            return View(users);
        }
        public async Task<IActionResult> Create(CancellationToken cToken)
        {


            var model = new CreateViewModel()
            {
                Roles = Enum.GetValues(typeof(RoleEnum))
               .Cast<RoleEnum>()

               .Select(r => new SelectListItem
               {
                   Value = ((int)r).ToString(),
                   Text = r.ToString()
               }).ToList()

            };
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel user, CancellationToken cToken)
        {
            if (!ModelState.IsValid)
            {
                user.Roles = GetRolesList();
                return View(user);
            }



            var user1 = new UserDto { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, UserName = user.Username, Email = user.Email, Password = user.Password, CityId = user.cityId, RoleId = user.RoleId, Mobile = user.Mobile, Address = user.Address, ProfileImgFile = user.ProfileImgFile, ImagePath = user.ImagePath };
            var result = await _UserSAppServices.Register(user1, cToken);

            if (result.Succeeded)
            {

                return RedirectToAction("Index", "Users");
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cToken)
        {
            var user = await _UserSAppServices.GetById(id, cToken);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                Role = user.Role,
                Mobile = user.Mobile,
                Statuse = user.Status,
                Username = user.UserName,
                cityId = user.CityId,
                RoleId = user.RoleId,
                
                ProfileImgFile = user.ProfileImgFile,
                Roles = GetRolesList()
            };
            return View(model);


        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModel user, CancellationToken cToken)
        {
            if (ModelState.IsValid)
            {
                user.Roles = GetRolesList();
                var user1 = new UserDto { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, UserName = user.Username, Email = user.Email, CityId = user.cityId, RoleId = user.RoleId, Mobile = user.Mobile, Address = user.Address, Status = user.Statuse, Role = user.Role, ProfileImgFile = user.ProfileImgFile };
                var result = await _UserSAppServices.Update(user1, cToken);
                if (result.IsSuccess)
                {
                   
                    TempData["SuccessMessage"] = result.IsMessage;


                }
                else
                {
                    TempData["ErrorMessage"] = result.IsMessage;

                }
                return RedirectToAction("Index", "Users");
            }
            user.Roles = GetRolesList();
            return View(user);


        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cToken)
        {

            var result = await _UserSAppServices.Delete(id, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;

                _logger.LogInformation(result.IsMessage, id);
            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;
                _logger.LogWarning("حذف کاربر با شناسه {UserId} ناموفق بود. دلیل: {ErrorMessage}", id, result.IsMessage);
            }
            return RedirectToAction("Index", "Users");

        }
        private List<SelectListItem> GetRolesList()
        {
            return Enum.GetValues(typeof(RoleEnum))
                .Cast<RoleEnum>()

                .Select(r => new SelectListItem
                {
                    Value = ((int)r).ToString(),
                    Text = r.ToString()
                })
                .ToList();
        }
        [HttpPost]
        public async Task<IActionResult> ActiveUser(int id, CancellationToken cToken)
        {
            var result = await _UserSAppServices.ActiveUser(id, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;


            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;
                _logger.LogWarning(" کاربر با شناسه {UserId} وجود ندارد: {ErrorMessage}", id, result.IsMessage);
            }
            return RedirectToAction("Index", "Users");

        }


    }
}

