namespace theCoffeeroom.Interfaces
{
    public interface IUtilityRepo
    {
        Task<string> EncodeHtml(string HtmlString);
        Task<string> Encrypt(string PassString);
        Task<string> GenerateOTP(int Digits);
    }
}
