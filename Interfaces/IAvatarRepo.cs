using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Interfaces
{
    public interface IAvatarRepo
    {
        Task<List<Avatar>> GetAvatarsAsync();
    }
}
