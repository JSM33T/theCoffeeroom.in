using Microsoft.AspNetCore.Mvc;
using theCoffeeroom.Models.Frame;

namespace theCoffeeroom.Controllers.Routers
{
    public class GalleryController : Controller
    {

       

        [Route("/gallery")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/gallery/{Slug}")]
        public IActionResult Frame(string Slug)
        {
            var viewModel = new GalleryLoad { Slug = Slug };
            return View(viewModel);
        }




    }
}
