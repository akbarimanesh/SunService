using Microsoft.Extensions.Caching.Memory;
using SunService.Domain.Core.SunServices.BaseEntities.AppServices;
using SunService.Domain.Core.SunServices.BaseEntities.Services;
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
    public class CategoryAppServices : ICategoryAppServices
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IBaseEntitiesServices _baseEntitiesServices;
        private readonly IMemoryCache _memoryCache;
        public CategoryAppServices(ICategoryServices categoryServices, IBaseEntitiesServices baseEntitiesServices, IMemoryCache memoryCache)
        {
            _categoryServices = categoryServices;
            _baseEntitiesServices = baseEntitiesServices;
            _memoryCache = memoryCache;
        }

        public async Task<Result> CreateCategory(CategoryDto category, CancellationToken cancellationToken)
        {
           
            if (category.ProfileImgFile is not null)
            {
                category.ImagePath = await _baseEntitiesServices.UploadImage(category.ProfileImgFile!, "Category", cancellationToken);
            }
            if (await _categoryServices.GetTitleCategory(category.Title, cancellationToken))
            {
                return new Result(false, "دسته بندی موجود می باشد.");

            }
            else
            {
                var category1 = new Category
                {
                    Title = category.Title,
                    Id = category.Id,
                  
                    ImagePath = category.ImagePath

                };
                await _categoryServices.CreateCategory(category, cancellationToken);
                return new Result(true, "دسته بندی با موفقیت اضافه شد.");
            }
        }

        public async Task<Result> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            if (await _categoryServices.GetCategoryById(id, cancellationToken) != null)
            {
                await _categoryServices.DeleteCategory(id, cancellationToken);
                return new Result(true, "با موفقیت حذف شد.");
            }
            else
                return new Result(false, " همچین دسته بندی وجود ندارد.");
        }

        public async Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
        {
            List<CategoryDto> categories;

            if (_memoryCache.Get("CategoriesList") is not null)
            {
                categories = _memoryCache.Get<List<CategoryDto>>("CategoriesList");

            }
            else
            {
                categories = await _categoryServices.GetAllCategories(cancellationToken);
                _memoryCache.Set("CategoriesList", categories, TimeSpan.FromHours(2));
            }




            return categories;

           
        }

        public async Task<List<Category>> GetAllCategoriesWithHomeservice(CancellationToken cancellationToken)
        {
            return await _categoryServices.GetAllCategoriesWithHomeservice(cancellationToken);
        }

        public async Task<Category> GetCategoryById(int id, CancellationToken cancellationToken)
        {
            if (await _categoryServices.GetCategoryById(id, cancellationToken) != null)
            {
                return await _categoryServices.GetCategoryById(id, cancellationToken);

            }
            else
                return null;
        }

        public async Task<Result> UpdateCategory(CategoryDto category, CancellationToken cancellationToken)
        {
            if (category.ProfileImgFile is not null)
            {
                category.ImagePath = await _baseEntitiesServices.UploadImage(category.ProfileImgFile!, "Category", cancellationToken);
            }
        

           
                await _categoryServices.UpdateCategory(category, cancellationToken);
                return new Result(true, "ویرایش با موفقیت انجام شد.");
           
        }
    }
}
