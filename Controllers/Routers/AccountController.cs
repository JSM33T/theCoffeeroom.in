using Microsoft.AspNetCore.Mvc;

namespace theCoffeeroom.Controllers.Routers
{
    public class AccountController : Controller
    {
        //account vue app
        // login/signup/signin/forgetpass/otp
        [Route("/account")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/account/signup")]
        public IActionResult SignUp()
        {
            return View("Views/Account/Index.cshtml");
        }

        [Route("/account/logout")]
        public void LogOut()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/");
        }
    }
}
