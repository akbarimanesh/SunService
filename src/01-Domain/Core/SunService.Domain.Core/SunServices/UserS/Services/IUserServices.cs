using SunService.Domain.Core.SunServices.UserS.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.Services
{
    public interface IUserServices
    {
        public Task<int> GetCount(CancellationToken cancellationToken);
        public Task<List<UserSummaryDto>> GetAll(CancellationToken cancellationToken);
        public Task<UserDto> GetById(int id, CancellationToken cancellationToken);
        public Task<bool> Update(UserDto model, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task Delete(int id, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task SaveImageUser(int id, string imagepath, CancellationToken cancellationToken);
    }
}
