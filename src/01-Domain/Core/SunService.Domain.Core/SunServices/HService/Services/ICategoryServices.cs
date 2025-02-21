using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.Services
{
    public interface ICategoryServices
    {
        public Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken);
        public Task<Category> GetCategoryById(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateCategory(CategoryDto category, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteCategory(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateCategory(CategoryDto category, CancellationToken cancellationToken);
        public Task<bool> GetTitleCategory(string categoyTitle, CancellationToken cToken);
    }
}
