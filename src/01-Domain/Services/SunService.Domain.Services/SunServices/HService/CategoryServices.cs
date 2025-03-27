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
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IDapperRepository _dapperRepository;
        public CategoryServices(ICategoryRepository categoryRepository, IDapperRepository dapperRepository)
        {
            _CategoryRepository = categoryRepository;
            _dapperRepository = dapperRepository;
        }

        public async Task CreateCategory(CategoryDto category, CancellationToken cancellationToken)
        {
            await _CategoryRepository.CreateCategory(category, cancellationToken);
        }

        public async Task DeleteCategory(int id, CancellationToken cancellationToken)
        {
            await _CategoryRepository.DeleteCategory(id, cancellationToken);
        }

        public async Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
        {
            return await _dapperRepository.GetAllCategories(cancellationToken);
        }

        public async Task<List<Category>> GetAllCategoriesWithHomeservice(CancellationToken cancellationToken)
        {
            return await _CategoryRepository.GetAllCategoriesWithHomeservice(cancellationToken);
        }

        public async Task<Category> GetCategoryById(int id, CancellationToken cancellationToken)
        {
            return await _CategoryRepository.GetCategoryById(id, cancellationToken);
        }

        public async Task<bool> GetTitleCategory(string categoyTitle, CancellationToken cToken)
        {
            return await _CategoryRepository.GetTitleCategory(categoyTitle, cToken);
        }

        public async Task UpdateCategory(CategoryDto category, CancellationToken cancellationToken)
        {
            await _CategoryRepository.UpdateCategory(category, cancellationToken);
        }
    }
}
