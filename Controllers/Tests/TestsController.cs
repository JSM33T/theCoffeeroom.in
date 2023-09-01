using Microsoft.AspNetCore.Mvc;
using theCoffeeroom.Core;

namespace theCoffeeroom.Controllers.Tests
{
    public class GrabACookie
    {
        public string Ckies{ get; set; }
    }
    public class TestsController : Controller
    {

        [Route("/test")]
        public IActionResult Main()
        {
                return View("Views/Tests/Index.cshtml");
        }

        [Route("/testfallback")]
        public IActionResult Index()
        {
            // Path to the fallback HTML file
            string fallbackFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fallback.html");

            if (System.IO.File.Exists(fallbackFilePath))
            {
                // Read and serve the fallback HTML file
                return PhysicalFile(fallbackFilePath, "text/html");
            }
            else
            {
                return NotFound(); // Fallback file not found
            }
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
        public IActionResult Something()
        {
           // string renderedView = await this.RenderViewAsync("Views/Tests/Haha.cshtml");

            // Now you can use the renderedView for your purpose (e.g., sending emails)

            return View("Views/Tests/Haha.cshtml");
        }

        [Route("api/encrypt")]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public  string Encryptor([FromBody] GrabACookie grabACookie)
        {
            string res = EnDcryptor.Encrypt(grabACookie.Ckies);
            return res;
        }

        [Route("api/decrypts")]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public string Decryptor([FromBody] GrabACookie grabACookie)
        {
            string res2 = EnDcryptor.Decrypt(grabACookie.Ckies);
            return res2;
        }

    }


}
