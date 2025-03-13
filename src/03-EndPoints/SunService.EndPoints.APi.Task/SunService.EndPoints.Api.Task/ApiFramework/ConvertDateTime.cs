using System.Globalization;

namespace SunService.EndPoints.Api.Task.ApiFramework
{
    public static class ConvertDateTime
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return $"{persianCalendar.GetYear(date)}/{persianCalendar.GetMonth(date):00}/{persianCalendar.GetDayOfMonth(date):00}";
        }
    }
}
