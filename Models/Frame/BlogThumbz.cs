namespace theCoffeeroom.Models.Frame
{
    public class BlogThumbz
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlHandle { get; set; }
        public string PostContent { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public string Yr { get; set; }
        public string Locator { get; set; }
        public string PostLikes { get; set; }
        public int Comments { get; set; }
        public DateTime DatePosted { get; set; }
        public int Id { get; internal set; }
    }
}
