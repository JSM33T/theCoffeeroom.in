using Microsoft.AspNetCore.Mvc;

namespace theCoffeeroom.Controllers.Routers
{
    public class AppController : Controller
    {

        [Route("fontsapp")]
        public IActionResult GoogleFonts()
        {
            return View("Views/Apps/Content/GoogleFontPlayground/Index.cshtml");
        }
        [Route("apps")]
        public IActionResult Index()
        {
            return View("Views/Apps/Index.cshtml");
        }
    }
}
