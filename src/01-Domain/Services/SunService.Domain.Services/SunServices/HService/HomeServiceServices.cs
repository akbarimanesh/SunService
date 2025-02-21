using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SunService.Domain.Services.SunServices.HService
{
    public class HomeServiceServices : IHomeServiceServices
    {
        private readonly IHomeServiceRepository _HomeServiceRepository;

        public HomeServiceServices(IHomeServiceRepository homeServiceRepository)
        {
            _HomeServiceRepository = homeServiceRepository;
        }

        public async Task CreateHomeService(HomeServiceDto homeService, CancellationToken cancellationToken)
        {
           await _HomeServiceRepository.CreateHomeService(homeService, cancellationToken);  
        }

        public async Task DeleteHomeService(int id, CancellationToken cancellationToken)
        {
            await _HomeServiceRepository.DeleteHomeService(id, cancellationToken);
        }

        public async Task<List<HomeServiceDto>> GetAllHomeService(CancellationToken cancellationToken)
        {
            return await _HomeServiceRepository.GetAllHomeService(cancellationToken);   
        }

        public async Task<HomeService> GetHomeServiceById(int id, CancellationToken cancellationToken)
        {
            return await _HomeServiceRepository.GetHomeServiceById(id, cancellationToken);  
        }

        public async Task<bool> GetTitleHomeService(string homeServiceTitle, CancellationToken cToken)
        {
            return await _HomeServiceRepository.GetTitleHomeService(homeServiceTitle, cToken);
        }

        public async Task UpdateHomeService(HomeServiceDto homeService, CancellationToken cancellationToken)
        {
            await _HomeServiceRepository.UpdateHomeService(homeService, cancellationToken); 
        }
    }
}
