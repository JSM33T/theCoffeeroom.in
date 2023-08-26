using System.ComponentModel.DataAnnotations;

namespace theCoffeeroom.Models.Frame
{
    public class LoginCreds
    {
        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public string Password { get; set; }

        public string Otp { get; set; }
    }
}
