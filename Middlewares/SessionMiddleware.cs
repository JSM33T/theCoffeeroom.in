using Microsoft.Data.SqlClient;
using Serilog;
using theCoffeeroom.Services.Helpers;

namespace theCoffeeroom.Middlewares
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Cookies.TryGetValue("SessionKey", out string cookieValue))
            {
                using SqlConnection connection = new(ConfigHelper.NewConnectionString);
                await connection.OpenAsync();
                SqlCommand checkcommand = new("SELECT p.*, a.Image " +
                    "FROM TblUserProfile p, TblAvatarMaster a " +
                    "WHERE SessionKey = @sessionkey" +
                    " AND p.IsActive = 1 " +
                    " AND p.IsVerified = 1 " +
                    " AND p.AvatarId = a.Id", connection);
                checkcommand.Parameters.AddWithValue("@sessionkey", cookieValue);
                using var reader = await checkcommand.ExecuteReaderAsync();

                if (reader.Read())
                {
                    // Extract user data and set it in the session
                    var username = reader.GetString(reader.GetOrdinal("UserName"));
                    var user_id = reader.GetInt32(reader.GetOrdinal("Id"));
                    var firstname = reader.GetString(reader.GetOrdinal("FirstName"));
                    var fullname = reader.GetString(reader.GetOrdinal("FirstName")) + " " + reader.GetString(reader.GetOrdinal("LastName"));
                    var role = reader.GetString(reader.GetOrdinal("Role"));
                    var avatar = reader.GetString(reader.GetOrdinal("Image"));

                    context.Session.SetString("user_id", user_id.ToString());
                    context.Session.SetString("username", username);
                    context.Session.SetString("first_name", firstname);
                    context.Session.SetString("role", role);
                    context.Session.SetString("fullname", fullname);
                    context.Session.SetString("avatar", avatar.ToString());
                    Log.Information("no session data set  for " + user_id.ToString());

                }
                
            }
            else
            {
                Log.Information("no session data present");
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
