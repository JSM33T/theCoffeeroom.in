using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Controllers.Routers
{
    public class ProfileController : Controller
    {
        
        [Route("/members")]
        public IActionResult Index()
        {
            string connectionString = ConfigHelper.NewConnectionString;
            var sessionStat = HttpContext.Session.GetString("username");
            if (sessionStat != null)
            {
                    return View("Views/Profiles/Index.cshtml");
            }
            else
            {
                return View("Views/Home/AccessDenied.cshtml");
            }

        }

        [Route("/member/{username?}")]
        public IActionResult Member()
        {
            string connectionString = ConfigHelper.NewConnectionString;
            var sessionStat = HttpContext.Session.GetString("username");
            if (sessionStat != null)
            {
                return View("Views/Profiles/Index.cshtml");
            }
            else
            {
                return View("Views/Home/AccessDenied.cshtml");
            }

        }

    }
}
