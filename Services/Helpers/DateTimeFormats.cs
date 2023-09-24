namespace theCoffeeroom.Services.Helpers
{
    public class DateTimeFormats
    {
        public static string FormatDateOrRelative(DateTime targetDate)
        {
            TimeSpan timeDifference = DateTime.Now - targetDate;
            if (timeDifference.TotalHours <= 23)
            {
                int hoursDiff= (int)Math.Floor(timeDifference.TotalHours);
                return hoursDiff + " Hours ago";
                //log and check the o/p
            }
            else if (timeDifference.TotalDays < 7)
            {
                int daysDifference = (int)Math.Floor(timeDifference.TotalDays);
                return daysDifference + " days ago";
            }
            else
            {
                return targetDate.ToString("MMMM dd, yyyy");
            }
        }

        public static string PrettifyDate(DateTime targetDate)
        {
            TimeSpan timeDifference = DateTime.Now - targetDate;
            if (timeDifference.TotalHours <= 23)
            {
                int hoursDiff = (int)Math.Floor(timeDifference.TotalHours);
                return hoursDiff + " Hours ago";
                //log and check the o/p
            }
            else if (timeDifference.TotalDays < 7)
            {
                int daysDifference = (int)Math.Floor(timeDifference.TotalDays);
                return daysDifference + " days ago";
            }
            else
            {
                return targetDate.ToString("MMMM dd, yyyy");
            }
        }
    }
}
