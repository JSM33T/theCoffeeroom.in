using Microsoft.AspNetCore.Mvc;

namespace theCoffeeroom.Controllers.Routers
{
    public class MusicController : Controller
    {
        [Route("studio/{suba?}")]
        public IActionResult Index()
        {
            return View("Views/Studio/Index.cshtml");
        }


    }
}
