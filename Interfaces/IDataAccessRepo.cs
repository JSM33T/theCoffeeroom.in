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
        
        Task<IEnumerable<Blog>> GetAllBlogsAsync();
        Task<Blog> GetBlogAsync(int Id);
        Task<Blog> AddBlogAsync(Blog blog);
        Task<Blog> UpdateBlogAsync(Blog blog);
        Task<Blog> DeleteBlogAsync(int Id);

        Task<IEnumerable<Blog>> GetAllBlogCatAsync();
        Task<BlogCat> GetBlogCatAsync(int Id);
        Task<BlogCat> AddBlogCatAsync(BlogCat blogCat);
        Task<BlogCat> UpdateBlogCatAsync(BlogCat blogCat);
        Task<BlogCat> DeleteBlogCatAsync(int Id);

        Task<IEnumerable<UserProfile>> GetAllUserProfiles();
        Task<UserProfile> GetUserProfileAsync(int Id);
        Task<UserProfile> AddUserProfileAsync(UserProfile userProfile);
        Task<UserProfile> UpdateUserProfileAsync(UserProfile userProfile);
        Task<UserProfile> DeleteUserProfileAsync(int Id);


    }
}
