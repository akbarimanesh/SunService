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
        private readonly IDapperRepository _dapperRepository;
        public SubCategoryServices(ISubCategoryRepository subCategoryRepository, IDapperRepository dapperRepository)
        {
            _SubCategoryRepository = subCategoryRepository;
            _dapperRepository = dapperRepository;
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
            return await _dapperRepository.GetAllSubCategories(cancellationToken);
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
