using System.ComponentModel.DataAnnotations;

namespace theCoffeeroom.Models.Frame
{
    public class GenericReceiver
    {
        public int IntRec{ get; set; }

        [Required]
        public string StringRec { get; set; }
        public decimal DeciRec { get; set; }
    }
}
