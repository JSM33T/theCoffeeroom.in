using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;
using theCoffeeroom.Models.Frame;

namespace theCoffeeroom.Controllers.Routers
{
    public class AccountController : Controller
    {

        [Route("/account")]
        public IActionResult Account()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return View("Views/Account/Index.cshtml");
            }
            else
            {
                return View("Views/Home/AccessDenied.cshtml");
            }
            
        }

        [Route("/account/signup")]
        public IActionResult SignUp()
        {
            return View("Views/Account/Index.cshtml");
        }

        [Route("/account/login")]
        public IActionResult Login()
        {
            return View("Views/Account/Index.cshtml");
        }

        [Route("/account/verification/{Uzrnm}/{OTP}")]
        public async Task<IActionResult> Verification(string Uzrnm,string OTP)
        {

            VerificationModel UserDetailsDisplay = null;
            string connectionString = ConfigHelper.NewConnectionString;
            using SqlConnection connection = new(connectionString);

            try{
                connection.Open();
                SqlCommand checkcmd = new("select * from TblUserProfile where Username ='" + Uzrnm + "' and OTP ='" + OTP + "' and IsVerified = 0 ", connection);
                SqlDataReader dataReader = await checkcmd.ExecuteReaderAsync();
                if (dataReader.Read())
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
            catch{
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
            HttpContext.Session.Clear();
            Response.Redirect("/");
        }
    }
}
