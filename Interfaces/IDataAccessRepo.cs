using theCoffeeroom.Models.DAModels;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Interfaces
{
    public interface IDataAccessRepo
    {
        Task<IEnumerable<Mail>> GetAllMailsAsync();
        Task<Mail> GetMailAsync(int Id);
        Task<DataSave> AddMailAsync(Mail mail);
        Task<Mail> UpdateMailAsync(Mail mail);
        Task<Mail> DeleteMailAsync(int Id);
        
       


    }
}
