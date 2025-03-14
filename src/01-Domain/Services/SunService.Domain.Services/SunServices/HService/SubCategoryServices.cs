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
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly ISubCategoryRepository _SubCategoryRepository;

        public SubCategoryServices(ISubCategoryRepository subCategoryRepository)
        {
            _SubCategoryRepository = subCategoryRepository;
        }

        public async Task CreateSubCategory(SubCategory subcategory, CancellationToken cancellationToken)
        {
            await _SubCategoryRepository.CreateSubCategory(subcategory, cancellationToken);
        }

        public async Task DeleteSubCategory(int id, CancellationToken cancellationToken)
        {
            await _SubCategoryRepository.DeleteSubCategory(id, cancellationToken);  
        }

        public async Task<List<SubCategoryDto>> GetAllSubCategories(CancellationToken cancellationToken)
        {
            return await _SubCategoryRepository.GetAllSubCategories(cancellationToken);
        }

        public async Task<List<SubCategoryDto>> GetSubCategoriesByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            return await _SubCategoryRepository.GetSubCategoriesByCategoryId(categoryId, cancellationToken);  
        }

        public async Task<SubCategory> GetSubCategoryById(int id, CancellationToken cancellationToken)
        {
            return await _SubCategoryRepository.GetSubCategoryById(id, cancellationToken);
        }

        public async Task<bool> GetTitleSubCategory(string subcategoyTitle, CancellationToken cToken)
        {
            return await _SubCategoryRepository.GetTitleSubCategory(subcategoyTitle, cToken);
        }

        public async Task UpdateSubCategory(SubCategoryDto subcategory, CancellationToken cancellationToken)
        {
            await _SubCategoryRepository.UpdateSubCategory(subcategory, cancellationToken); 
        }
    }
}
