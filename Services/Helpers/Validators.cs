using System.Net.Mail;
using System.Text.RegularExpressions;

namespace theCoffeeroom.Services.Helpers
{
    public class Validators
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsAlphaNumeric(string input)
        {
            Regex regex = new("^[a-zA-Z0-9]+$");
            return regex.IsMatch(input);
        }
    }
}
