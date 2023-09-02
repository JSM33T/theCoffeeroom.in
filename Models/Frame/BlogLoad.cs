namespace theCoffeeroom.Models.Frame
{
    public class BlogLoad
    {
        public int Id { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Year{ get; set; }
        public string Likes { get; internal set; }
    }
}
