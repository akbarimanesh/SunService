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
        private readonly IDapperRepository _dapperRepository;
        public HomeServiceServices(IHomeServiceRepository homeServiceRepository, IDapperRepository dapperRepository)
        {
            _HomeServiceRepository = homeServiceRepository;
            _dapperRepository = dapperRepository;
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
            return await _dapperRepository.GetAllHomeService(cancellationToken);
        }

        public async Task<HomeService> GetHomeServiceById(int id, CancellationToken cancellationToken)
        {
            return await _HomeServiceRepository.GetHomeServiceById(id, cancellationToken);  
        }

        public async Task<List<HomeServiceDto>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)
        {
            return await _HomeServiceRepository.GetHomeServicesBySubCategoryId(subCategoryId, cancellationToken);   
        }

        public async Task<bool> GetTitleHomeService(string homeServiceTitle, CancellationToken cToken)
        {
            return await _HomeServiceRepository.GetTitleHomeService(homeServiceTitle, cToken);
        }

        public async Task<List<HomeService>> SearchServices(string title, CancellationToken cancellationToken)
        {
           return await _HomeServiceRepository.SearchServices(title, cancellationToken);
        }

        public async Task UpdateExpertServices(int expertId, List<int> selectedHomeServices, CancellationToken cancellationToken)
        {
            await _HomeServiceRepository.UpdateExpertServices(expertId, selectedHomeServices, cancellationToken);   
        }

        public async Task UpdateHomeService(HomeServiceDto homeService, CancellationToken cancellationToken)
        {
            await _HomeServiceRepository.UpdateHomeService(homeService, cancellationToken); 
        }
    }
}
