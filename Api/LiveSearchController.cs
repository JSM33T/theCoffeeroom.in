using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;
using theCoffeeroom.Models.Domain;
using theCoffeeroom.Services.Helpers;

namespace theCoffeeroom.Api
{
    public class LiveSearchController : Controller
    {
        [HttpGet]
        [Route("/api/livesearch/{Type?}/{SearchKey?}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnGetEngineOilList(string Type, string SearchKey)
        {
            try
            {
                List<Search> entries = new();
                string sql;
                string connectionString = ConfigHelper.NewConnectionString;
                using (SqlConnection connection = new(connectionString))
                {
                    await connection.OpenAsync();
                    sql = "select * from TblSearchMaster where Title like '%" + SearchKey + "%' or Description like '%" + SearchKey + "%' ";

                    using SqlCommand command = new(sql, connection);
                    command.Parameters.AddWithValue("@SString", "%" + SearchKey + "%");
                    command.Parameters.AddWithValue("@Type", Type);
                    using SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (await dataReader.ReadAsync())
                    {
                        Search entry = new()
                        {
                            Title = (string)dataReader["Title"],
                            Description = (string)dataReader["Description"],
                            Image = (string)dataReader["Image"],
                            Url = (string)dataReader["Url"],
                            Type = (string)dataReader["Type"]
                        };
                        entries.Add(entry);
                    }
                    await connection.CloseAsync();
                }
                return new JsonResult(entries);
            }
            catch (Exception ex)
            {

                Log.Error("error in live search: " + ex.Message.ToString());
                return BadRequest("something went wrong");
            }
        }
    }
}
