using System;

namespace Gryffindor.Web.Helpers
{
    public static class Helper
    {
        public static bool IsImage(string fileName)
        {
            fileName = fileName.ToLower();
            return
                fileName.EndsWith(".jpg") ||
                fileName.EndsWith(".jpeg") ||
                fileName.EndsWith(".png") ||
                fileName.EndsWith(".bmp");
        }

        public static string GetUsernameFromEmail(string email)
        {
            return email.Trim().Split('@')[0].Replace(".", "");
        }

        public static bool IsDateRangeValid(DateTime? dateFrom, DateTime? dateTo, bool allowFutureDate = false)
        {
            dateTo = dateTo == null ? DateTime.Now : dateTo;
            if (!allowFutureDate)
                return dateTo <= DateTime.Now;
            return dateFrom < dateTo;
        }
    }
}