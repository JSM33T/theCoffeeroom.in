using Microsoft.AspNetCore.Components.Forms;

namespace theCoffeeroom.Models.Domain
{
    public class TgBot
    {
        public long UserId { get; set; }
        public string Message { get; set; }
        public string ImgAddress{ get; set; }
        public string ImgAddress1 { get; set; }
        public string ImgAddress2 { get; set; }
        public string Btn1Text { get; set; }
        public string Btn1Url { get; set; }
        public string Btn2Text { get; set; }
        public string Btn2Url { get; set; }
        public string Token { get; set; }
    }
}
