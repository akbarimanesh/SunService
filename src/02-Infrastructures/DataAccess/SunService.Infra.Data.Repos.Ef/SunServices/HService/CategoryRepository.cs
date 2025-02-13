using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.AppServices.SunServices.HService
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateCategory(Category category, CancellationToken cancellationToken)
        {
            await _appDbContext.Categories.AddAsync(category,cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var category = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Categories.Remove(category);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
        {
            return await _appDbContext.Categories.AsNoTracking().Select(x => new CategoryDto()
            {
                Id = x.Id,
                Title = x.Title,
                ImagePath=x.ImagePath

            }).ToListAsync(cancellationToken);

        }

        public async Task<Category> GetCategoryById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
        }

        public async Task UpdateCategory(Category category, CancellationToken cancellationToken)
        {
            var category1 = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id,cancellationToken);
            category1.Id = category.Id;
            category1.Title = category.Title;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
