using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.AppServices
{
    public interface ISubCategoryAppServices
    {
        public Task<List<SubCategoryDto>> GetAllSubCategories(CancellationToken cancellationToken);
        public Task<SubCategory> GetSubCategoryById(int id, CancellationToken cancellationToken);
        public Task<Result> CreateSubCategory(SubCategory subcategory, CancellationToken cancellationToken);
        public Task<Result> DeleteSubCategory(int id, CancellationToken cancellationToken);
        public Task<Result> UpdateSubCategory(SubCategoryDto subcategory, CancellationToken cancellationToken);
    }
}
