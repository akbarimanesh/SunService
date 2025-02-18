using Microsoft.AspNetCore.Identity;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.AppServices
{
    public interface IUserSAppServices
    {
        public Task<IdentityResult> Register(UserDto model, CancellationToken cToken);
        public Task<IdentityResult> Login(string username, string password, CancellationToken cToken);
    }
}
