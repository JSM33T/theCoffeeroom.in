using Microsoft.Data.SqlClient;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Repositories
{
    public class AvatarRepo
    {
        public string connectionString = ConfigHelper.NewConnectionString;
        public async Task<List<Avatar>> GetAvatarsAsync()
        {
            using SqlConnection connection = new(connectionString);
            await connection.OpenAsync();

            string sql = "SELECT * FROM TblAvatarMaster";

            using SqlCommand command = new(sql, connection);
            using SqlDataReader dataReader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            List<Avatar> entries = new List<Avatar>();

            while (await dataReader.ReadAsync())
            {
                Avatar entry = new Avatar
                {
                    Id = dataReader["Id"] as int? ?? 0,
                    Title = dataReader["Title"] as string ?? "",
                    Image = dataReader["Image"] as string ?? ""
                };
                entries.Add(entry);
            }

            return entries;
        }
    }
}
