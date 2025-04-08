using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.AppServices
{
    public interface ICategoryAppServices
    {
        public Task<List<Category>> GetAllCategoriesWithHomeservice(CancellationToken cancellationToken);
        public Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken);
        public Task<Category> GetCategoryById(int id, CancellationToken cancellationToken);
        public Task<Result> CreateCategory(CategoryDto category, CancellationToken cancellationToken);
        public Task<Result> DeleteCategory(int id, CancellationToken cancellationToken);
        public Task<Result> UpdateCategory(CategoryDto category, CancellationToken cancellationToken);
        public void ClearCategoryCache();
    }
}
