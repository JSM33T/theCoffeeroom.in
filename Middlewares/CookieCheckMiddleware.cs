using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Threading.Tasks;
using theCoffeeroom.Services.Helpers;

public class CookieCheckMiddleware
{
    private readonly RequestDelegate _next;

    public CookieCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Session.GetString("username") == null)   
        {
            //Log.Information("no session found");
            if (context.Request.Cookies.ContainsKey("SessionKey"))
            {
             //   Log.Information("remember me active");
                context.Request.Cookies.TryGetValue("SessionKey", out string cookieValue);
                using SqlConnection connection = new(ConfigHelper.NewConnectionString);
                await connection.OpenAsync();
                SqlCommand checkcommand = new("select p.*,a.Image " +
                    "from TblUserProfile p,TblAvatarMaster a " +
                    "WHERE SessionKey = @sessionkey" +
                    " and p.IsActive= 1 " +
                    " and p.IsVerified = 1 " +
                    " and p.AvatarId = a.Id", connection);
                checkcommand.Parameters.AddWithValue("@sessionkey", cookieValue);
                using var reader = await checkcommand.ExecuteReaderAsync();
                if (reader.Read())
                {
                    var username = reader.GetString(reader.GetOrdinal("UserName"));
                    var user_id = reader.GetInt32(reader.GetOrdinal("Id"));
                    var firstname = reader.GetString(reader.GetOrdinal("FirstName"));
                    var fullname = reader.GetString(reader.GetOrdinal("FirstName")) + " " + reader.GetString(reader.GetOrdinal("LastName"));
                    var role = reader.GetString(reader.GetOrdinal("Role"));
                    var avatar = reader.GetString(reader.GetOrdinal("Image"));
                    //set session
                    context.Session.SetString("user_id", user_id.ToString());
                    context.Session.SetString("username", username);
                    context.Session.SetString("first_name", firstname);
                    context.Session.SetString("role", role);
                    context.Session.SetString("fullname", fullname);
                    context.Session.SetString("avatar", avatar.ToString());
                }
            }
           
        }
        else
        {
          //  Log.Information("session active");
        }
        
        await _next(context);
    }
}

// Extension method to add the middleware to the HTTP request pipeline
public static class CookieCheckMiddlewareExtensions
{
    public static IApplicationBuilder UseCookieCheckMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CookieCheckMiddleware>();
    }
}
