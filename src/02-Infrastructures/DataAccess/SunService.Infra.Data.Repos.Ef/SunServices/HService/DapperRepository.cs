using Dapper;
using Microsoft.Data.SqlClient;
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
    public class DapperRepository : IDapperRepository
    {
       
        private readonly string _connectionString;
        public DapperRepository(IOptions<SiteSettings> siteSettings)
        {
            _connectionString = siteSettings.Value.ConnectionStrings.SqlConnection;
           
        }
        public async Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var categories = await connection.QueryAsync<CategoryDto>(QuerysSundb.GetAllCategories);
                return categories.ToList();
            }
        }

        public async Task<List<HomeServiceDto>> GetAllHomeService(CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var homeservices = await connection.QueryAsync<HomeServiceDto>(QuerysSundb.GetAllHomeServices);
                return homeservices.ToList();
            }
        }

        public async Task<List<SubCategoryDto>> GetAllSubCategories(CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var subcategories = await connection.QueryAsync<SubCategoryDto>(QuerysSundb.GetAllSubCategories);
                return subcategories.ToList();
            }
        }

        public async Task<List<City>> GetCities(CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return (await connection.QueryAsync<City>(QuerysSundb.GetAllcities)).ToList();
            }
        }
    }
}
