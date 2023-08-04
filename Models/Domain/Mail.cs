using System.ComponentModel.DataAnnotations;

namespace theCoffeeroom.Models.Domain
{
    public class Mail
    {
        [Required]
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        public string EMailId { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string IsActive { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
