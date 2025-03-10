using Microsoft.AspNetCore.Identity;
using SunService.Domain.Core.SunServices.HService.DTOs;
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
        public Task<List<int>> GetHomeServicesExpert(int expertid, CancellationToken CancellationToken);
        public Task<Expert> GetExpert(int id, CancellationToken cancellationToken);
        public Task<Result> ActiveUser(int id, CancellationToken cToken);
        public Task<Result> DeActiveUser(int id, CancellationToken cToken);
        public Task<Expert> GetExpertById(int homeServiceId, CancellationToken cancellationToken);
        public Task<int> GetCount(CancellationToken cancellationToken);
        public Task<List<UserSummaryDto>> GetAll(CancellationToken cancellationToken);
        public Task<UserDto> GetById(int id, CancellationToken cancellationToken);
        public Task<Result> Update(UserDto model, CancellationToken cancellationToken);
        public Task<Result> Delete(int id, CancellationToken cancellationToken);
        public Task<IdentityResult> Register(UserDto model, CancellationToken cToken);
        public Task<IdentityResult> Login(string username, string password, CancellationToken cToken);
    }
}
