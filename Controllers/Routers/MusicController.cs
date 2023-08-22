using Microsoft.AspNetCore.Mvc;

namespace theCoffeeroom.Controllers.Routers
{
    public class MusicController : Controller
    {
        [Route("music/{suba?}")]
        public IActionResult Index()
        {
            return View("Views/Music/Index.cshtml");
        }


    }
}
