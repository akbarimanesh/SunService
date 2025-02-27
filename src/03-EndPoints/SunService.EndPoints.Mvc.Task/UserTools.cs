using System.Security.Claims;

namespace SunService.EndPoints.Mvc.Task
{
    public static class UserTools
    {
        public static string GetRole(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
        }
        public static string GetCityId(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type == "CityId").Value;
        }
        public static string GetEmail(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type == "Email").Value;
        }

        public static string GetUserName(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type == "Username").Value;
        }
    }
}
