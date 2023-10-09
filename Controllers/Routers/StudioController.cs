using Microsoft.AspNetCore.Mvc;

namespace theCoffeeroom.Controllers.Routers
{
    public class StudioController : Controller
    {
        [Route("studio")]
        public IActionResult Index()
        {
            return View("Views/Studio/Index.cshtml");
        }

        [Route("music")]
        public IActionResult Music()
        {
            return View("Views/Studio/Music/Index.cshtml");
        }

        [Route("music/album/{slug}")]
        public IActionResult Albums(string Slug)
        {
            return View("Views/Studio/Music/Content/Albums/" + Slug +".cshtml");
        }

        [Route("music/single/{slug}")]
        public IActionResult Singles(string Slug)
        {
            return View("Views/Studio/Music/Content/Singles/" + Slug + ".cshtml");
        }


    }
}
