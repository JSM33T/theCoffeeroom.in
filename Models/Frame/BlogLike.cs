namespace theCoffeeroom.Models.Frame
{
    public class BlogLike
    {
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public bool IsLiked { get; set; }
        public string Slug { get; set; }
    }
}
