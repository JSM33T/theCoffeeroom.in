using Microsoft.Data.SqlClient;
using Serilog;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Interfaces;
using theCoffeeroom.Models.DAModels;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Repositories
{
    public class DataAccessRepo : IDataAccessRepo
    {
        public string connectionString = ConfigHelper.NewConnectionString;

        //=====================================ADD========================================
        public Task<Blog> AddBlogAsync(Blog blog)
        {
            throw new NotImplementedException();
        }

        public Task<BlogCat> AddBlogCatAsync(BlogCat blogCat)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSave> AddMailAsync(Mail mail)
        {
            using SqlConnection connection = new(connectionString);
            await connection.OpenAsync();

            string checkEmailQuery = "SELECT COUNT(*) FROM TblMailingList WHERE Email = @Email";
            SqlCommand checkEmailCmd = new(checkEmailQuery, connection);
            checkEmailCmd.Parameters.AddWithValue("@Email", mail.EMailId);
            int emailCount = (int)await checkEmailCmd.ExecuteScalarAsync();
            try
            {
                if (emailCount == 0)
                {
                    string getMaxIdQuery = "SELECT MAX(Id) FROM TblMailingList";
                    SqlCommand getMaxIdCmd = new(getMaxIdQuery, connection);
                    int maxId = (int)await getMaxIdCmd.ExecuteScalarAsync();
                    int newId = maxId + 1;

                    string addEmailQuery = "INSERT INTO TblMailingList (Id, Email,Origin,DateAdded) VALUES (@id, @email,@origin,@dateadded)";
                    SqlCommand addEmailCmd = new(addEmailQuery, connection);
                    addEmailCmd.Parameters.AddWithValue("id", newId);
                    addEmailCmd.Parameters.AddWithValue("@email", mail.EMailId);
                    addEmailCmd.Parameters.AddWithValue("@origin", mail.Origin);
                    addEmailCmd.Parameters.AddWithValue("@dateadded", DateTime.Now);
                    await addEmailCmd.ExecuteNonQueryAsync();
                    Log.Information("mail added to newsletter:" + mail.EMailId);

                    return new DataSave 
                    {
                        Status = true,
                        Message = "Email saved successfully",
                        Remark = mail.EMailId.ToString()
                    };
                  
                }
                else
                {
                    return new DataSave 
                    { 
                        Status = false,
                        Message = "Email is already submitted",
                        Remark = mail.EMailId.ToString()
                    };
                }
            }
            catch(Exception ex)
            {
                Log.Error("email da repo error" + ex.Message.ToString());
                throw new Exception("something went wrong");
            }
        }

        public Task<UserProfile> AddUserProfileAsync(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }


        //=====================================DELETE========================================
        public Task<Blog> DeleteBlogAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogCat> DeleteBlogCatAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Mail> DeleteMailAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfile> DeleteUserProfileAsync(int Id)
        {
            throw new NotImplementedException();
        }


        //=====================================GET LIST========================================
        public Task<IEnumerable<Blog>> GetAllBlogCatAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Mail>> GetAllMailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserProfile>> GetAllUserProfiles()
        {
            throw new NotImplementedException();
        }


        //=====================================GET ITEM BY ID========================================
        public Task<Blog> GetBlogAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogCat> GetBlogCatAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Mail> GetMailAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfile> GetUserProfileAsync(int Id)
        {
            throw new NotImplementedException();
        }



        //=====================================UPDATE========================================
        public Task<Blog> UpdateBlogAsync(Blog blog)
        {
            throw new NotImplementedException();
        }

        public Task<BlogCat> UpdateBlogCatAsync(BlogCat blogCat)
        {
            throw new NotImplementedException();
        }

        public Task<Mail> UpdateMailAsync(Mail mail)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfile> UpdateUserProfileAsync(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }
    }
}
