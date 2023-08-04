using theCoffeeroom.Interfaces;

namespace theCoffeeroom.Repositories
{
    public class UtilityRepo : IUtilityRepo
    {
        public Task<string> EncodeHtml(string HtmlString)
        {
            throw new NotImplementedException();
        }

        public Task<string> Encrypt(string PassString)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateOTP(int Digits)
        {
            throw new NotImplementedException();
        }
    }
}
