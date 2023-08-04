using System.ComponentModel.DataAnnotations;

namespace theCoffeeroom.Models.Frame
{
    public class LoginCreds
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
