namespace theCoffeeroom.Models.Domain
{
    public class Theme
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CuratorId { get; set; }
        public string CuratorName { get; set; }
        public string String { get; set; }
        public string FontLink { get; set; }
        public string CoverImage { get; set; }
        public string PrimaryCol { get; set; }
        public string SecondaryCol { get; set; }
    }
}
