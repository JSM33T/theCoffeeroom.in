using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;
using theCoffeeroom.Models.Frame;

namespace theCoffeeroom.Controllers.Dedicated
{
    public class AccountController : Controller
    {
        [HttpPost("/api/account/login")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UserLogin([FromBody] LoginCreds loginCreds)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }

            string connectionString = ConfigHelper.NewConnectionString;
            try
            {
                using SqlConnection connection = new(connectionString);
                await connection.OpenAsync();
                SqlCommand checkcommand = new("select p.*,a.Image " +
                    "from TblUserProfile p,TblAvatarMaster a " +
                    "where UserName = @username " +
                    "and PassWord =@password " +
                    "and p.IsActive= 1 " +
                    "and p.IsVerified = 1 " +
                    "and p.AvatarId = a.Id", connection);
                checkcommand.Parameters.AddWithValue("@username", loginCreds.UserName.ToLower());
                checkcommand.Parameters.AddWithValue("@password", loginCreds.Password);
                using (var reader = await checkcommand.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        var username = reader.GetString(reader.GetOrdinal("UserName"));
                        var user_id = reader.GetInt32(reader.GetOrdinal("Id"));
                        var firstname = reader.GetString(reader.GetOrdinal("FirstName"));
                        var fullname = firstname + " " + reader.GetString(reader.GetOrdinal("LastName"));
                        var role = reader.GetString(reader.GetOrdinal("Role"));
                        var avatar = reader.GetString(reader.GetOrdinal("Image"));
                        //set session
                        HttpContext.Session.SetString("user_id", user_id.ToString());
                        HttpContext.Session.SetString("username", username);
                        HttpContext.Session.SetString("first_name", firstname);
                        HttpContext.Session.SetString("role", role);
                        HttpContext.Session.SetString("fullname", fullname);
                        HttpContext.Session.SetString("avatar", avatar.ToString());
                        return Ok("logging in...");
                    }
                    else
                    {
                        Log.Information("invalid creds by username:" + loginCreds.UserName);
                      
                    }
                }
                await connection.CloseAsync();

                return BadRequest("Invalid Credentials");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString() + " : in login form,user : " + loginCreds.UserName);
                return StatusCode(500, "soomething went wrong");
               
            }
        }
    }
}
