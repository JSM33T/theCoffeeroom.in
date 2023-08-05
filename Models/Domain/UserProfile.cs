using System.ComponentModel.DataAnnotations;

namespace theCoffeeroom.Models.Domain
{
    public class UserProfile
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int? AvatarId { get; set; }
        public string Bio { get; set; }
        public DateTime? DateJoined { get; set; }
        public DateTime? DateEdited { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
