using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Controllers.Routers
{
    public class ProfileController : Controller
    {
        [Route("/profile/{level1?}")]
        public async Task<IActionResult> Index()
        {
            UserProfile userProfile = null;
            string connectionString = ConfigHelper.NewConnectionString;
            var sessionStat = HttpContext.Session.GetString("role");
            if (sessionStat != null)
            {
                if (sessionStat.ToString() == "user" || sessionStat.ToString() == "admin")
                {
                    using var connection = new SqlConnection(connectionString);
                    await connection.OpenAsync();
                    var command = new SqlCommand("SELECT * FROM TblUserProfile WHERE UserName = @Id", connection);
                    command.Parameters.AddWithValue("@Id", HttpContext.Session.GetString("username"));
                    var reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        userProfile = new UserProfile()
                        {
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            UserName = reader.GetString(3),
                            Role = reader.GetString(11),
                            EMail = reader.GetString(4),
                            Phone = reader.GetString(5),
                            Gender = reader.GetString(6),
                            Bio = reader.GetString(11),
                            AvatarId = reader.GetInt32(12)
                        };
                        await reader.CloseAsync();
                    }
                    await connection.CloseAsync();
                    return View(userProfile);

                }
                else
                {
                    return View("Views/Home/AccessDenied.cshtml");
                }

            }
            else
            {
                return View("Views/Home/AccessDenied.cshtml");
            }
            
        }
        
    }
}
