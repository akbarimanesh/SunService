using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserServices _UserServices;
        private readonly IBaseEntitiesServices _baseEntitiesServices;
        private readonly ICustomerServices _customerServices;
        private readonly IExpertServices _expertServices;
        public UserSAppServices(SignInManager<User> signInManager, UserManager<User> userManager, IPasswordHasher<User> passwordHasher, IUserServices userServices, IBaseEntitiesServices baseEntitiesServices, ICustomerServices customerServices, IExpertServices expertServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _UserServices = userServices;
            _baseEntitiesServices = baseEntitiesServices;
            _customerServices = customerServices;
            _expertServices = expertServices;
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
                return null;
        }

        public async Task<int> GetCount(CancellationToken cancellationToken)
        {
            return await _UserServices.GetCount(cancellationToken);
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
                
                FirstName=model.FirstName,
                LastName=model.LastName,
                Address=model.Address,
                Mobile=model.Mobile,
                RegisterAt=DateTime.Now,
                StatusUser=model.Status,
                UserName = model.UserName,
                Email= model.Email,
                RoleId=model.RoleId,
                CityId = model.CityId,
                ImagePath=model.ImagePath,
                
            };
           

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if ((RoleEnum)user.RoleId == RoleEnum.Admin)
                {
                    role = "Admin";
                }

                if ((RoleEnum)user.RoleId == RoleEnum.Customer)
                {
                    role = "Customer";
                    var customer = new Customer
                    {
                        Id = user.Id,
                    };

                    await _customerServices.CreateCustomer(customer, cToken);

                }

                if ((RoleEnum)user.RoleId == RoleEnum.Expert)
                {
                    role = "Expert";
                    var expert = new Expert
                    {
                        Id = user.Id,
                    };
                    await _expertServices.CreateExpert(expert, cToken);
                }
                var createdUser = await _userManager.FindByNameAsync(user.UserName);
                
                if (model.ProfileImgFile is not null)
                {
                    int userId = user.Id;
                    model.ImagePath = await _baseEntitiesServices.UploadImage(model.ProfileImgFile!, "Profiles", cToken);
                    await _UserServices.SaveImageUser(userId, model.ImagePath, cToken);
                }

                await _userManager.AddToRoleAsync(user, role);


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