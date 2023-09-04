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

        [Route("studio/album/{slug}")]
        public IActionResult Albums(string Slug)
        {
            return View("Views/Studio/Content/Albums/" + Slug +".cshtml");
        }

        [Route("studio/single/{slug}")]
        public IActionResult Singles(string Slug)
        {
            return View("Views/Studio/Content/Singles/" + Slug + ".cshtml");
        }


    }
}
