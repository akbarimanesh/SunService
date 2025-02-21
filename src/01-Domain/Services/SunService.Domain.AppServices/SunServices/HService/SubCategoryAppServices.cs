using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.AppServices.SunServices.HService
{
    public class SubCategoryAppServices : ISubCategoryAppServices
    {
        private readonly ISubCategoryServices _subCategoryServices;

        public SubCategoryAppServices(ISubCategoryServices subcategoryServices)
        {
            _subCategoryServices = subcategoryServices;
        }

        public async Task<Result> CreateSubCategory(SubCategory subcategory, CancellationToken cancellationToken)
        {
            if (await _subCategoryServices.GetTitleSubCategory(subcategory.Title, cancellationToken))
            {
                return new Result(false, "دسته بندی موجود می باشد.");

            }

            else
            {
                var subcategory1 = new SubCategory
                {
                    Title = subcategory.Title,
                    CategoryId= subcategory.CategoryId,
                    HomeServices= subcategory.HomeServices,

                };
                await _subCategoryServices.CreateSubCategory(subcategory1, cancellationToken);
                return new Result(true, "دسته بندی با موفقیت اضافه شد.");
            }
        }

        public async Task<Result> DeleteSubCategory(int id, CancellationToken cancellationToken)
        {
            if (await _subCategoryServices.GetSubCategoryById(id, cancellationToken) != null)
            {
                await _subCategoryServices.DeleteSubCategory(id, cancellationToken);
                return new Result(true, "با موفقیت حذف شد.");
            }
            else
                return new Result(false, " همچین زیردسته بندی وجود ندارد.");
        }

        public async Task<List<SubCategoryDto>> GetAllSubCategories(CancellationToken cancellationToken)
        {
            return await _subCategoryServices.GetAllSubCategories(cancellationToken);
        }

        public async Task<SubCategory> GetSubCategoryById(int id, CancellationToken cancellationToken)
        {
            if (await _subCategoryServices.GetSubCategoryById(id, cancellationToken) != null)
            {
                return await _subCategoryServices.GetSubCategoryById(id, cancellationToken);

            }
            else
                return null;
        }

        public async Task<Result> UpdateSubCategory(SubCategoryDto subcategory, CancellationToken cancellationToken)
        {
            
                await _subCategoryServices.UpdateSubCategory(subcategory, cancellationToken);
                return new Result(true, "ویرایش با موفقیت انجام شد.");
           
        }
    }
}
