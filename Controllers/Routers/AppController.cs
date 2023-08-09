using Microsoft.AspNetCore.Mvc;

namespace theCoffeeroom.Controllers.Routers
{
    public class AppController : Controller
    {

        [Route("fontsapp")]
        public IActionResult Index()
        {
            return View("Views/Apps/Content/GoogleFontPlayground/Index.cshtml");
        }
    }
}
