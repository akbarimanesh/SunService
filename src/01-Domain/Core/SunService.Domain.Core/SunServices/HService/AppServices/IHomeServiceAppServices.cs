using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.AppServices
{
    public interface IHomeServiceAppServices
    {
       public Task<List<HomeServiceDto>> SearchServices(string title, CancellationToken cancellationToken);

        public Task<List<HomeServiceDto>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken);
        public Task<Result> UpdateExpertServices(int expertId, List<int> selectedHomeServices, CancellationToken cancellationToken);
        public Task<List<HomeServiceDto>> GetAllHomeService(CancellationToken cancellationToken);
        public Task<HomeService> GetHomeServiceById(int id, CancellationToken cancellationToken);
        public Task<Result> CreateHomeService(HomeServiceDto homeService, CancellationToken cancellationToken);

        public Task<Result> DeleteHomeService(int id, CancellationToken cancellationToken);
        public Task<Result> UpdateHomeService(HomeServiceDto homeService, CancellationToken cancellationToken);
        public void ClearHomeserviceCache();

    }
}
