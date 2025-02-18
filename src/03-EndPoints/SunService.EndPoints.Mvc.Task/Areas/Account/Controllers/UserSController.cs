using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.Core.SunServices.BaseEntities.AppServices;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.EndPoints.Mvc.Task.Areas.Account.Models;
using System.Threading;

namespace SunService.EndPoints.Mvc.Task.Areas.Account.Controllers
{
    [Area("Account")]
   
    public class UserSController : Controller
    {
        private readonly IUserSAppServices _UserSAppServices;
        private readonly SignInManager<User> _signInManager;
        private readonly IBaseDataAppService _baseDataAppService;

        public UserSController(IUserSAppServices userSAppServices, SignInManager<User> signInManager, IBaseDataAppService baseDataAppService)
        {
            _UserSAppServices = userSAppServices;
            _signInManager = signInManager;
            _baseDataAppService = baseDataAppService;
        }
     
     
        public async Task<IActionResult> Register(CancellationToken cToken)
        {
            

            

            return View(); 
            
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel user, CancellationToken cToken)
        {
            if (!ModelState.IsValid)
                return View(user);
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password) )
            {
                ModelState.AddModelError(string.Empty, "فیلدهای خالی رو پر کنید.");
                return View(user);
            }
            var user1 = new UserDto { UserName = user.Username, Password = user.Password};
            var result = await _UserSAppServices.Register(user1, cToken);

            if (result.Succeeded)
            {

                return RedirectToAction("Login", "UserS");
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }



           

            return RedirectToAction("Login", "UserS");
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
                return RedirectToAction("Index", "Home", new { area = "" });
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

 
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}

