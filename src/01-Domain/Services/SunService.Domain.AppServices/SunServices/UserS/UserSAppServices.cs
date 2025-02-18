using Microsoft.AspNetCore.Identity;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Enums;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserSAppServices(SignInManager<User> signInManager, UserManager<User> userManager, IPasswordHasher<User> passwordHasher)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }


        public async Task<IdentityResult> Login(string username, string password, CancellationToken cToken)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);
            return result.Succeeded ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> Register(UserDto model, CancellationToken cToken)
        {
            string role = string.Empty;
            var user = new User
            {
                UserName = model.UserName,
              

                CityId = model.CityId,

            };
            if (model.Role == RoleEnum.Admin)
            {
                role = "Admin";
            }

            if (model.Role == RoleEnum.Customer)
            {
                role = "Customer";

            }

            if (model.Role == RoleEnum.Expert)
            {
                role = "Expert";

            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //if (model.ProfileImgFile is not null)
                //{
                //    model.ImagePath = await _baseDataService.UploadImage(model.ProfileImgFile!, "Profiles", cancellationToken);
                //}

                await _userManager.AddToRoleAsync(user, role);


                if (model.Role == RoleEnum.Customer)
                {
                    await _userManager.AddClaimAsync(user, new Claim("CustomerId", user.RoleId.ToString()));
                }

                if (model.Role == RoleEnum.Expert)
                {
                    await _userManager.AddClaimAsync(user, new Claim("ExpertId", user.RoleId.ToString()));
                }

                await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);

            }

            return result;

        }
    }
}