using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task CreateHomeService(HomeService homeService, CancellationToken cancellationToken)
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

        public async Task UpdateHomeService(HomeService homeService, CancellationToken cancellationToken)
        {
            await _HomeServiceRepository.UpdateHomeService(homeService, cancellationToken); 
        }
    }
}
