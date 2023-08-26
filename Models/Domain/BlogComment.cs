using System.ComponentModel.DataAnnotations;

namespace theCoffeeroom.Models.Domain
{
    public class BlogComment
    {
        public int Id { get; set; }

        [MaxLength(200)]
        [MinLength(2)]
        public string Comment { get; set; }

        public string DateCommented { get; set; }

        public int UserId { get; set; }

        public string UsersName { get; set; }

        public string UserName { get; set; }

        public string Slug { get; set; }
    }
}
