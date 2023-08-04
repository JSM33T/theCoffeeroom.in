using Microsoft.AspNetCore.Mvc;

namespace theCoffeeroom.Controllers.Routers
{
    public class MusicController : Controller
    {
        [Route("/music/{sub_a?}/{sub_b?}")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("{type?}/{Year?}/{slug?}")]
        public IActionResult Track()
        {
            return View();
        }

    }
}
