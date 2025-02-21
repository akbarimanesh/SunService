using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.Data
{
    public interface IHomeServiceRepository
    {
        public Task<List<HomeServiceDto>> GetAllHomeService(CancellationToken cancellationToken);
        public Task<HomeService> GetHomeServiceById(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateHomeService(HomeServiceDto homeService, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteHomeService(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateHomeService(HomeServiceDto homeService, CancellationToken cancellationToken);
        public Task<bool> GetTitleHomeService(string homeServiceTitle, CancellationToken cToken);
    }
}
