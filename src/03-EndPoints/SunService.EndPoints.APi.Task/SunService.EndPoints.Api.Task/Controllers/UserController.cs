using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using SunService.Domain.AppServices.SunServices.UserS;
using SunService.Domain.Core.SunServices.BaseEntities.AppServices;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Enums;
using Microsoft.AspNetCore.Identity.Data;
using SunService.Domain.Core.SunServices.UserS.AppServices;

namespace SunService.EndPoints.Api.Task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserSAppServices _UserSAppServices;
        private readonly ILogger<UserController> _logger;
        private readonly IBaseDataAppService _BaseDataAppService;
        public UserController(UserManager<User> userManager, IUserSAppServices userSAppServices, ILogger<UserController> logger, IBaseDataAppService baseDataAppService)
        {
            _userManager = userManager;
            _UserSAppServices = userSAppServices;
            _logger = logger;
            _BaseDataAppService = baseDataAppService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model, CancellationToken cToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest(new { Message = "فیلدهای خالی را پر کنید." });
            }
            if (model.RoleId == (int)RoleEnum.Admin)
            {
                return BadRequest(new { Message = "شما نمی‌توانید خود را به عنوان مدیر ثبت‌نام کنید." });
            }
            var userDto = new UserDto
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                CityId = model.CityId,
                RoleId = model.RoleId
            };
            var result = await _UserSAppServices.Register(userDto, cToken);
            if (result.Succeeded)
            {
                _logger.LogInformation("ثبت نام موفق: کاربر {Username} با موفقیت ثبت نام شد.", model.UserName);
                return Ok(new { message = "ثبت نام موفقیت‌آمیز بود." });
            }
            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description); 
            }

            return BadRequest(new { Message = "خطایی در ثبت نام رخ داده است.", Errors = result.Errors });


        }
    }
}
