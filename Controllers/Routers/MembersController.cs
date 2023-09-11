using Microsoft.AspNetCore.Mvc;

namespace theCoffeeroom.Controllers.Routers
{
    public class MembersController : Controller
    {
        [Route("/members/{Classify?}")]
        public IActionResult MemberList()
        {
            return View("Views/Members/Index.cshtml");
        }

        //vue app
        [Route("/member/{MemberUserName}")]
        public IActionResult MemberVue()
        {
            return View("Views/Members/Index.cshtml");
        }
    }
}
