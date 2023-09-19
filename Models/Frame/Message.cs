namespace theCoffeeroom.Models.Frame
{
    public class Message
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string MsgText { get; set; }
        public string Origin { get; set; }
        public DateTime DatePosted { get; set; }

    }
}
