using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using theCoffeeroom.Services;
using theCoffeeroom.Models.Domain;
using theCoffeeroom.Models.Frame;
using theCoffeeroom.Services.Helpers;

namespace theCoffeeroom.Controllers.Routers
{
    public class AccountController : Controller
    {
        readonly string connectionString = ConfigHelper.NewConnectionString;

        [Route("/account")]
        public IActionResult Account()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return View("Views/Account/Index.cshtml");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Route("/account/{subroute}")]
        public async Task<IActionResult> SignUp()
        {
            if (HttpContext.Request.Cookies.TryGetValue("SessionKey", out string cookieValue))
            {
                using SqlConnection connection = new(connectionString);
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
                    HttpContext.Session.SetString("user_id", user_id.ToString());
                    HttpContext.Session.SetString("username", username);
                    HttpContext.Session.SetString("first_name", firstname);
                    HttpContext.Session.SetString("role", role);
                    HttpContext.Session.SetString("fullname", fullname);
                    HttpContext.Session.SetString("avatar", avatar.ToString());
                    Response.Redirect("/");
                    return View("Views/Account/Index.cshtml");
                }
                else
                {
                    Response.Cookies.Delete("UserLoginCookie");
                    return View("Views/Account/Index.cshtml");
                }
               
            }
            else
            {
                return View("Views/Account/Index.cshtml");
            }
            
        }

        [Route("/account/verification/{Uzrnm}/{OTP}")]
        public async Task<IActionResult> Verification(string Uzrnm,string OTP)
        {
            string connectionString = ConfigHelper.NewConnectionString;
            using SqlConnection connection = new(connectionString);

            VerificationModel UserDetailsDisplay;
            try
            {
                connection.Open();
                SqlCommand checkcmd = new("select * from TblUserProfile where Username ='" + Uzrnm + "' and OTP ='" + OTP + "' and IsVerified = 0 ", connection);
                SqlDataReader dataReader = await checkcmd.ExecuteReaderAsync();
                if(dataReader.Read())
                {
                    UserDetailsDisplay = new()
                    {
                        FirstName = dataReader.GetString(1),
                        Stat = "valid"
                    };
                    await dataReader.CloseAsync();
                    SqlCommand activatecmd = new("UPDATE TblUserProfile SET IsVerified = 1 where UserName ='" + Uzrnm + "' ", connection);
                    await activatecmd.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    return View("Views/Account/Verification.cshtml", UserDetailsDisplay);
                }
                else
                {
                    UserDetailsDisplay = new()
                    {
                        FirstName = "na",
                        Stat = "invalid"
                    };
                    return View("Views/Account/Verification.cshtml", UserDetailsDisplay);
                }
            }
            catch
            {
                UserDetailsDisplay = new()
                {
                    FirstName = "na",
                    Stat = "invalid"
                };
                return View("Views/Account/Verification.cshtml", UserDetailsDisplay);
            }
        }

        [Route("/account/logout")]
        public void LogOut()
        {
            Response.Cookies.Delete("SessionKey");
            HttpContext.Session.Clear();
            Response.Redirect("/");
        }
    }
}
