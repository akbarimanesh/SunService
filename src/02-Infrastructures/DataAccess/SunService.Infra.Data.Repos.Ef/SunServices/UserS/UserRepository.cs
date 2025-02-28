using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.UserS.Data;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Enums;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Repos.Ef.SunServices.UserS
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task ActiveUser(int id, CancellationToken cToken)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cToken);
            user.StatusUser = true;
            await _appDbContext.SaveChangesAsync(cToken);
        }

        public async Task DeActiveUser(int id, CancellationToken cToken)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cToken);
            user.StatusUser = false;
            await _appDbContext.SaveChangesAsync(cToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var model = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Users.Remove(model);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<UserSummaryDto>> GetAll(CancellationToken cancellationToken)
        {
            var users = await _appDbContext.Users
           .Select(u => new UserSummaryDto
           {
               Id = u.Id,
               FirstName=u.FirstName,
               LastName=u.LastName,
               UserName = u.UserName,
               Mobile = u.Mobile,
               Email = u.Email,
               RegisterAt = u.RegisterAt,
               City = u.City.Title,
               Role = (RoleEnum)u.RoleId,
               StatusUser = u.StatusUser,
               ImagePath = u.ImagePath
           }).ToListAsync(cancellationToken);

            return users;
        }

        public async Task<UserDto> GetById(int id, CancellationToken cancellationToken)
        {
            var user =await _appDbContext.Users
            .Include(x => x.City)
           .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

            if (user is null) throw new Exception("user not found");

            var result = new UserDto();

            result.Id = user.Id;
            result.FirstName = user.FirstName;
            result.LastName = user.LastName;
            result.UserName = user.UserName;
            result.Mobile = user.Mobile;
            result.Email = user.Email;
            result.Address = user.Address;
            result.CityId = user.City.Id;
            result.Role = (RoleEnum)user.RoleId;
            result.RoleId = user.RoleId;
            result.ImagePath = user.ImagePath;
            result.Status = user.StatusUser;
            return result;

        }

        public async Task<int> GetCount(CancellationToken cancellationToken)
        {
            return await _appDbContext.Users.CountAsync(cancellationToken);
        }

        public async Task SaveImageUser(int id, string imagepath, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
            user.ImagePath = imagepath;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> StatusUser(string username, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
            bool x = user.StatusUser;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return x;
        }

        public async Task<bool> Update(UserDto model, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users
            .Include(x => x.City)
            .FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

            if (user is null) return false;
            user.RoleId= model.RoleId;
            user.Id = model.Id;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Mobile = model.Mobile;
            user.Email = model.Email;
            user.CityId = model.CityId;
            user.Address = model.Address;
            user.StatusUser = model.Status;
            user.ImagePath = model.ImagePath ?? user.ImagePath;
            
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
