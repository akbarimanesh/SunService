using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.Services
{
    public interface ISubCategoryServices
    {
        public Task<List<SubCategoryDto>> GetAllSubCategories(CancellationToken cancellationToken);
        public Task<SubCategory> GetSubCategoryById(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateSubCategory(SubCategory subcategory, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteSubCategory(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateSubCategory(SubCategory subcategory, CancellationToken cancellationToken);
    }
}
