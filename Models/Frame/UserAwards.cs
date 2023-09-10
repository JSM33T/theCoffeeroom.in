namespace theCoffeeroom.Models.Frame
{
    public class UserAwards
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
