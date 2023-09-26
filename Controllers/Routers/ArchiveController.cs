using Microsoft.AspNetCore.Mvc;

namespace theCoffeeroom.Controllers.Routers
{
    public class ArchiveController : Controller
    {
        [Route("/archive")]
        public IActionResult Index()
        {
            return View("Views/Archive/Index.cshtml");
        }
    }
}
