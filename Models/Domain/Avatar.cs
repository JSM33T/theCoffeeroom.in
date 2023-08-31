using System.ComponentModel.DataAnnotations;

namespace theCoffeeroom.Models.Domain
{
    public class Avatar
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
    }
}
