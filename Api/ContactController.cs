using Microsoft.AspNetCore.Mvc;

namespace theCoffeeroom.Api
{
    public class ContactController : Controller
    {
        [HttpPost("/api/message/text")]
        [IgnoreAntiforgeryToken]
        public IActionResult MessageMe()
        {
            return View();
        }
    }
}
