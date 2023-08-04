using Microsoft.AspNetCore.Mvc;
using theCoffeeroom.Models.Route;

namespace theCoffeeroom.Controllers.Routers
{

    public class BlogsController : Controller
    {   
        //Vue App
        [Route("/blogs/{sub_a?}/{sub_b?}")]
        public IActionResult Index()
        {
            return View();
        }


        //SEO
        [Route("/blog/{Year?}/{Slug}")]
        public IActionResult Blogs(string Year,string Slug)
        {
            BlogRoute blogRoute = new()
            {
                Year = Year,
                Slug = Slug

            };
            return View("Views/Blogs/Frame.cshtml",blogRoute);
        }


    }
}
