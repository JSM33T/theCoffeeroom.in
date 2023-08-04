using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using theCoffeeroom.Models;

namespace theCoffeeroom.Controllers.Routers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("/")]
        public IActionResult Index(string level)
        {
            return View(level);
        }

        [Route("/changelog")]
        public IActionResult Changelog()
        {
            return View();
        }

        [Route("/faq")]
        public IActionResult Faq()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}