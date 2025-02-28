using Microsoft.AspNetCore.Identity;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.Data
{
    public  interface IUserRepository
    {
        public Task<int> GetCount(CancellationToken cancellationToken);
        public Task<List<UserSummaryDto>> GetAll(CancellationToken cancellationToken);
        public Task<UserDto> GetById(int id, CancellationToken cancellationToken);
        public Task<bool> Update(UserDto model, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task Delete(int id, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task SaveImageUser(int id, string imagepath, CancellationToken cancellationToken);
            public Task<bool> StatusUser(string username, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task ActiveUser(int id, CancellationToken cToken);
        public System.Threading.Tasks.Task DeActiveUser(int id, CancellationToken cToken);
    }
}
