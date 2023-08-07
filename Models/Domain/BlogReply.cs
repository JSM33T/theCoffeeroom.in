namespace theCoffeeroom.Models.Domain
{
    public class BlogReply
    {
        public string Slug { get; set; }
        public string CommentId { get; set; }
        public string Reply { get; set; }
        public int ReplyId { get; set; }
        public int UserId { get; set; }
        public string ReplyText { get; set; }
    }
}
