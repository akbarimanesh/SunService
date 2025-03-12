using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Db.SqlServer.Dapper
{
    public static class QuerysSundb
    {
        public static string GetAllcities = "SELECT c.Id, c.Title FROM Cities AS c;";
        public static string GetAllCategories = "SELECT c.Id, c.Title, c.ImagePath FROM Categories AS c;";
        public static string GetAllSubCategories = "SELECT sc.Id, sc.Title, sc.CategoryId,  c.Title AS CategoryTitle, c.ImagePath  FROM SubCategories AS sc INNER JOIN Categories AS c ON c.Id = sc.CategoryId;";
        public static string GetAllHomeServices = "SELECT hs.Id, hs.Title, hs.Description, hs.BasePrice, hs.NumberVisits, hs.ImagePath, hs.SubCategoryId,   sc.Title AS SubCategoryTitle FROM HomeServices AS hs  INNER JOIN SubCategories AS sc ON sc.Id = hs.SubCategoryId INNER JOIN  Categories c ON sc.CategoryId = c.Id;";
       
       
    }
}
