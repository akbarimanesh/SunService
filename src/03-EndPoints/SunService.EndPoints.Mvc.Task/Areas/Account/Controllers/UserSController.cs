using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SunService.Domain.Core.SunServices.BaseEntities.AppServices;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Enums;
using SunService.EndPoints.Mvc.Task.Areas.Account.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading;

namespace SunService.EndPoints.Mvc.Task.Areas.Account.Controllers
{
    [Area("Account")]
   
    public class UserSController : Controller
    {
        private readonly IUserSAppServices _UserSAppServices;
        private readonly SignInManager<User> _signInManager;
        private readonly IBaseDataAppService _baseDataAppService;
        private readonly UserManager<User> _userManager;
        public UserSController(IUserSAppServices userSAppServices, SignInManager<User> signInManager, IBaseDataAppService baseDataAppService, UserManager<User> userManager)
        {
            _UserSAppServices = userSAppServices;
            _signInManager = signInManager;
            _userManager = userManager;
            _baseDataAppService = baseDataAppService;
        }
     
     
        public async Task<IActionResult> Register(CancellationToken cToken)
        {
            var model = new RegisterViewModel
            {
                Roles = GetRolesList() 
            };

            return View(model);

        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel user, CancellationToken cToken)
        {
            if (!ModelState.IsValid)
            {
                user.Roles = GetRolesList(); 
                return View(user);
            }

            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Email))
            {
                ModelState.AddModelError(string.Empty, "فیلدهای خالی رو پر کنید.");
                user.Roles = GetRolesList();
                return View(user);
            }
            if (user.RoleId == (int)RoleEnum.Admin)
            {
                ModelState.AddModelError(string.Empty, "شما نمی‌توانید خود را به عنوان مدیر ثبت ‌نام کنید.");
                return View(user);
            }
           
            var user1 = new UserDto { UserName = user.Username,Email=user.Email, Password = user.Password,CityId=user.cityId,RoleId=user.RoleId,ProfileImgFile=user.ProfileImgFile,ImagePath=user.ImagePath };
            var result = await _UserSAppServices.Register(user1, cToken);

            if (result.Succeeded)
            {

                return RedirectToAction("Login", "UserS");
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            user.Roles = GetRolesList();
            return View(user);
        }



     
        public async Task< IActionResult> Login(CancellationToken cToken)
        {

            

            return View();

           
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user, CancellationToken cToken)
        {
            if (!ModelState.IsValid)
                return View(user);
            if (string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Username))
            {
                ModelState.AddModelError(string.Empty, "فیلدهای خالی رو پر کنید.");
                return View(user);
            }
            var result = await _UserSAppServices.Login(user.Username, user.Password, cToken);

            if (result.Succeeded)
            {
                var appUser = await _userManager.FindByNameAsync(user.Username);
                var roles = await _userManager.GetRolesAsync(appUser);
                if (roles.Contains("Admin")) 
                {
                   
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                   
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است.");
            }

            return View(user);
        }



        public async Task<IActionResult> Index(CancellationToken cToken)
        {


            return View();


        }

        private List<SelectListItem> GetRolesList()
        {
            return Enum.GetValues(typeof(RoleEnum))
                .Cast<RoleEnum>()
                .Where(r => r != RoleEnum.Admin) // حذف نقش ادمین
                .Select(r => new SelectListItem
                {
                    Value = ((int)r).ToString(),
                    Text = r.ToString()
                })
                .ToList();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
       
    }
}

