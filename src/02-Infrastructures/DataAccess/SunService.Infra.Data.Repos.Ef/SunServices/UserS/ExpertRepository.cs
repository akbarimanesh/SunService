using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Data;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Repos.Ef.SunServices.UserS
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly AppDbContext _appDbContext;

        public ExpertRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateExpert(Expert expert, CancellationToken cancellationToken)
        {
            await _appDbContext.Experts.AddAsync(expert, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteExpert(int id, CancellationToken cancellationToken)
        {
            var expert = await _appDbContext.Experts.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Experts.Remove(expert);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ExpertDto>> GetAllExperts(CancellationToken cancellationToken)
        {
            return await _appDbContext.Experts.AsNoTracking().Select(x => new ExpertDto()
            {

                Id = x.Id,
                ExpertFullName = x.FirstName + " " + x.LastName,
                Address = x.Address,
                City = x.City.Title,
                Email = x.Email,
                Mobile = x.Mobile,
                Biography = x.Biography,
                ImagePath = x.ImagePath,
               
            }).ToListAsync(cancellationToken);
        }

        public async Task<Expert> GetExpertById(int homeServiceId, CancellationToken cancellationToken)
        {
            return await _appDbContext.ExpertServices
            .Include(es => es.Expert)
             .Where(es => es.HomeServiceId == homeServiceId)
             .Select(es => new Expert
             {
                 CityId = es.Expert.CityId
             })
             .FirstOrDefaultAsync(cancellationToken);

        }

        public async Task UpdateExpert(Expert expert, CancellationToken cancellationToken)
        {
            var expert1 = await _appDbContext.Experts.FirstOrDefaultAsync(x => x.Id == expert.Id, cancellationToken);
            expert1.Id = expert.Id;
            expert1.FirstName = expert.FirstName;
            expert1.LastName = expert.LastName;
            expert1.ShabaNumber = expert.ShabaNumber;
            expert1.CardNumber = expert.CardNumber;
            expert1.Address = expert.Address;
            expert1.Balance = expert.Balance;
            expert1.City = expert.City;
            expert1.Email = expert.Email;
            expert1.UserName = expert.UserName;
            expert1.PasswordHash = expert.PasswordHash;
            expert1.Mobile = expert.Mobile;
            expert1.Biography=expert.Biography;
            expert1.ImagePath= expert.ImagePath;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
