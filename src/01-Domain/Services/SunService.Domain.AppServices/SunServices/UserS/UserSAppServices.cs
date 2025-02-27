using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SunService.Domain.Core.SunServices.BaseEntities.Services;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Enums;
using SunService.Domain.Core.SunServices.UserS.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SunService.Domain.AppServices.SunServices.UserS
{

    public class UserSAppServices : IUserSAppServices
    {
        private readonly ILogger<UserSAppServices> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserServices _UserServices;
        private readonly IBaseEntitiesServices _baseEntitiesServices;
        private readonly ICustomerServices _customerServices;
        private readonly IExpertServices _expertServices;

        public UserSAppServices(SignInManager<User> signInManager, UserManager<User> userManager, IPasswordHasher<User> passwordHasher, IUserServices userServices, IBaseEntitiesServices baseEntitiesServices, ICustomerServices customerServices, IExpertServices expertServices, ILogger<UserSAppServices> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _UserServices = userServices;
            _baseEntitiesServices = baseEntitiesServices;
            _customerServices = customerServices;
            _expertServices = expertServices;
            _logger = logger;
        }

        public async Task<Result> ActiveUser(int id, CancellationToken cToken)
        {
            if (await _UserServices.GetById(id, cToken) != null)
            {
                await _UserServices.ActiveUser(id, cToken);
                return new Result(true, "کاربر فعال شد.");
            }
            else
                return new Result(false, "همچین کاربری وجود ندارد.");
        }

        public async Task<Result> Delete(int id, CancellationToken cancellationToken)
        {
            if (await _UserServices.GetById(id, cancellationToken) != null)
            {
                await _UserServices.Delete(id, cancellationToken);
                return new Result(true, "با موفقیت حذف شد.");
            }
            else
                return new Result(false, " همچین کاربری وجود ندارد.");
        }

        public async Task<List<UserSummaryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _UserServices.GetAll(cancellationToken);
        }

        public async Task<UserDto> GetById(int id, CancellationToken cancellationToken)
        {
            if (await _UserServices.GetById(id, cancellationToken) != null)
            {
                return await _UserServices.GetById(id, cancellationToken);

            }
            else
                _logger.LogWarning("کاربری با شناسه {UserId} یافت نشد.", id);
            return null;

        }

        public async Task<int> GetCount(CancellationToken cancellationToken)
        {
            return await _UserServices.GetCount(cancellationToken);
        }

        public async Task<IdentityResult> Login(string username, string password, CancellationToken cToken)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                _logger.LogWarning("ورود ناموفق: نام کاربری یا رمز عبور خالی ارسال شده است.");
                return IdentityResult.Failed(new IdentityError { Description = "نام کاربری و رمز عبور نباید خالی باشند." });
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                _logger.LogWarning("ورود ناموفق: کاربر با نام کاربری {Username} یافت نشد.", username);
                return IdentityResult.Failed(new IdentityError { Description = "کاربر یافت نشد." });
            }

            if (!await _UserServices.StatusUser(username, cToken))
            {
                _logger.LogInformation("ورود ناموفق: کاربر {Username} هنوز فعال نشده است.", username);
                return IdentityResult.Failed(new IdentityError { Description = "کاربر شما هنوز فعال نشده است." });
            }

            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

            if (!result.Succeeded)
            {
                _logger.LogWarning("ورود ناموفق: کاربر {Username} رمز عبور اشتباه وارد کرد.", username);
                return  IdentityResult.Failed(new IdentityError { Description = "نام کاربری یا رمز عبور اشتباه است." });
            }

            return IdentityResult.Success;
        }




        public async Task<IdentityResult> Register(UserDto model, CancellationToken cToken)
        {
            string role = string.Empty;
            var user = new User();
           
          
            if ((RoleEnum)model.RoleId == RoleEnum.Admin)
            {
                role = "Admin";

            }

            if ((RoleEnum)model.RoleId == RoleEnum.Customer)
            {
                role = "Customer";
                user = new Customer();
            }

            if ((RoleEnum)model.RoleId == RoleEnum.Expert)
            {
                role = "Expert";
                user = new Expert
                {
                    Biography = model.Biography
                };
            }

            user.Id = model.Id;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.Mobile = model.Mobile;
            user.RegisterAt = DateTime.Now;
            user.StatusUser = model.Status;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.RoleId = model.RoleId;
            user.CityId = model.CityId;
            user.ImagePath = model.ImagePath;







            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var createdUser = await _userManager.FindByNameAsync(user.UserName);

                if (model.ProfileImgFile is not null)
                {
                    int userId = user.Id;
                    model.ImagePath = await _baseEntitiesServices.UploadImage(model.ProfileImgFile!, "Profiles", cToken);
                    await _UserServices.SaveImageUser(userId, model.ImagePath, cToken);
                }

                await _userManager.AddToRoleAsync(user, role);
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
                await _userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));

                if ((RoleEnum)model.RoleId == RoleEnum.Customer)
                {
                    await _userManager.AddClaimAsync(user, new Claim("CustomerId", user.RoleId.ToString()));
                }

                if ((RoleEnum)model.RoleId == RoleEnum.Expert)
                {
                    await _userManager.AddClaimAsync(user, new Claim("ExpertId", user.RoleId.ToString()));
                }

                //  await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);

            }

            return result;

        }

        public async Task<Result> Update(UserDto model, CancellationToken cancellationToken)
        {
            if (model.ProfileImgFile is not null)
            {
                model.ImagePath = await _baseEntitiesServices.UploadImage(model.ProfileImgFile!, "Profiles", cancellationToken);
            }
            if (await _UserServices.GetById(model.Id, cancellationToken) != null)
            {
                await _UserServices.Update(model, cancellationToken);
                return new Result(true, "با موفقیت ویرایش شد.");
            }
            else
                return new Result(false, "همچین کاربری وجود ندارد.");
        }
    }
}