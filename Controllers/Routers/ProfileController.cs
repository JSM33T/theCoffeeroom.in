using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Controllers.Routers
{
    public class ProfileController : Controller
    {

        [Route("/profile/{level1?}")]
        public IActionResult Index()
        {
            string connectionString = ConfigHelper.NewConnectionString;
            var sessionStat = HttpContext.Session.GetString("username");
            if (sessionStat != null)
            {
                return View("Views/Profile/Index.cshtml");
            }
            else
            {
                return View("Views/Home/AccessDenied.cshtml");
            }

        }

    }
}
