using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.Task.Configs;
using SunService.Infra.Data.Db.SqlServer.Dapper;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Repos.Ef.SunServices.HService
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly string _connectionString;
        public SubCategoryRepository(IOptions<SiteSettings> siteSettings, AppDbContext appDbContext)
        {
            _connectionString = siteSettings.Value.ConnectionStrings.SqlConnection;
            _appDbContext = appDbContext;
        }
      

        public async Task CreateSubCategory(SubCategory subcategory, CancellationToken cancellationToken)
        {
            await _appDbContext.SubCategories.AddAsync(subcategory, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteSubCategory(int id, CancellationToken cancellationToken)
        {
            var subcategory = await _appDbContext.SubCategories.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.SubCategories.Remove(subcategory);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
      

        public async Task<List<SubCategoryDto>> GetSubCategoriesByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
             return await _appDbContext.SubCategories.Where(sc => sc.CategoryId == categoryId)
            .Select(sc => new SubCategoryDto
            {
                Id = sc.Id,
                Title = sc.Title,
                CategoryName=sc.Category.Title
            })
            .ToListAsync(cancellationToken);
        }

<<<<<<< HEAD
=======
        public async Task<List<SubCategoryDto>> GetSubCategoriesByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
             return await _appDbContext.SubCategories.Where(sc => sc.CategoryId == categoryId)
            .Select(sc => new SubCategoryDto
            {
                Id = sc.Id,
                Title = sc.Title,
                CategoryName=sc.Category.Title
            })
            .ToListAsync(cancellationToken);
        }

>>>>>>> 428ace9864b323b8f7437fedf5fea4be5a89b919
        //public async Task<List<SubCategoryDto>> GetAllSubCategories(CancellationToken cancellationToken)
        //{
        //    return await _appDbContext.SubCategories.AsNoTracking().Select(x => new SubCategoryDto()
        //    {
        //        Id = x.Id,
        //        Title = x.Title,
        //        CategoryName=x.Category.Title,

        //    }).ToListAsync(cancellationToken);
        //}

        public async Task<SubCategory> GetSubCategoryById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.SubCategories.AsNoTracking().Include(s => s.Category).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> GetTitleSubCategory(string subcategoyTitle, CancellationToken cToken)
        {
            return await _appDbContext.SubCategories.AsNoTracking().AnyAsync(t => t.Title == subcategoyTitle);
        }

        public async Task UpdateSubCategory(SubCategoryDto subcategory, CancellationToken cancellationToken)
        {
            var subcategory1 = await _appDbContext.SubCategories.FirstOrDefaultAsync(x => x.Id == subcategory.Id, cancellationToken);
            subcategory1.Id = subcategory.Id;
            subcategory1.Title = subcategory.Title;
           
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
