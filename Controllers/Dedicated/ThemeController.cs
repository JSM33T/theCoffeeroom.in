using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Controllers.Dedicated
{
    public class ThemeController : Controller
    {
        readonly string connectionString = ConfigHelper.NewConnectionString;
        [HttpGet("api/theme/{id}")]
        public async Task<IActionResult> GetThemeById(int id)
        {
            List<Theme> themes = new();
            
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string sql;
                sql = "SELECT * FROM TblThemeMaster WHERE Id=@Id";
                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@Id", id);
                using SqlDataReader dataReader = await command.ExecuteReaderAsync();

                while (await dataReader.ReadAsync())
                {
                    Theme entry = new()
                    {
                        Title = (string)dataReader["Title"],
                        Description = (string)dataReader["Description"],
                        String = (string)dataReader["String"],
                        FontLink = (string)dataReader["FontLink"],
                    };
                    themes.Add(entry);
                }
            }
            return new JsonResult(themes);
        }

        [HttpGet("api/themes/list")]
        public async Task<IActionResult> GetThemes()
        {
             List<Theme> ThemeDD;
            ThemeDD = new List<Theme>();
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("SELECT a.*,u.FirstName as CuratorName from TblThemeMaster a,TblUserProfile u where a.CuratorId = u.Id and a.IsActive = 1 order by Id", connection);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                ThemeDD.Add(new Theme
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    String = reader.GetString(reader.GetOrdinal("String")),
                    FontLink = reader.GetString(reader.GetOrdinal("FontLink")),
                    CuratorId = reader.GetInt32(reader.GetOrdinal("CuratorId")),
                    CuratorName = reader.GetString(reader.GetOrdinal("CuratorName")),
                    CoverImage = reader.GetString(reader.GetOrdinal("CoverImage")),
                    PrimaryCol = reader.GetString(reader.GetOrdinal("PrimaryCol")),
                });
            }
            await reader.CloseAsync();
            return new JsonResult(ThemeDD);
        }
    }
}
