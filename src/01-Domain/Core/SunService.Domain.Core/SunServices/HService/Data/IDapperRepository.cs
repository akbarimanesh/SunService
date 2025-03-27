using SunService.Domain.Core.SunServices.HService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.Data
{
    public interface IDapperRepository
    {
        public Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken);
        public Task<List<HomeServiceDto>> GetAllHomeService(CancellationToken cancellationToken);
        public Task<List<SubCategoryDto>> GetAllSubCategories(CancellationToken cancellationToken);
        Task<List<City>> GetCities(CancellationToken cancellationToken);
    }
}
