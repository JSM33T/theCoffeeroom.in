using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using theCoffeeroom.Services;

namespace theCoffeeroom.Controllers.Tests
{
    public class TestsController : Controller
    {


        [Route("/test")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/gallery/{Year}/{Slug}")]
        [IgnoreAntiforgeryToken]
        public IActionResult GetHTMLContent(string Year, string Slug)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "gallerycontent", "garden-state", "gallery.min.glr");

            if (System.IO.File.Exists(filePath))
            {
                var htmlContent = System.IO.File.ReadAllText(filePath);
                return Content(htmlContent, "text/html");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/tester")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Something()
        {
           // string renderedView = await this.RenderViewAsync("Views/Tests/Haha.cshtml");

            // Now you can use the renderedView for your purpose (e.g., sending emails)

            return View("Views/Tests/Haha.cshtml");
        }

    }


}
