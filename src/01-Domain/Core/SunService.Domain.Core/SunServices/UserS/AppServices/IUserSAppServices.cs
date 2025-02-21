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
        public Task<int> GetCount(CancellationToken cancellationToken);
        public Task<List<UserSummaryDto>> GetAll(CancellationToken cancellationToken);
        public Task<UserDto> GetById(int id, CancellationToken cancellationToken);
        public Task<Result> Update(UserDto model, CancellationToken cancellationToken);
        public Task<Result> Delete(int id, CancellationToken cancellationToken);
        public Task<IdentityResult> Register(UserDto model, CancellationToken cToken);
        public Task<IdentityResult> Login(string username, string password, CancellationToken cToken);
    }
}
