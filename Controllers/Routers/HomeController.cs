using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using theCoffeeroom.Models.Domain;
using theCoffeeroom.Models.Frame;
using theCoffeeroom.Services.Helpers;

namespace theCoffeeroom.Controllers.Routers
{

    public class HomeController : Controller
    {
        public string connectionString = ConfigHelper.NewConnectionString;
        [Route("/")]
        public IActionResult Index(string level)
        {
            return View(level);
        }

        [Route("/wall/{subLvla?}/{subLvlb?}/{subLvlc?}")]
        public IActionResult Wall()
        {
            return View("Views/Home/Wall.cshtml");
        }


        [Route("/docs")]
        public IActionResult DocsBase()
        {
            return View("Views/Docs/Index.cshtml");
        }


        [Route("/docs/changelog")]
        public IActionResult Changelog()
        {
            return View("Views/Docs/Changelog.cshtml");
        }

        [Route("/docs/faq")]
        public IActionResult Faq()
        {
            return View("Views/Docs/Faq.cshtml");
        }

        [Route("/docs/attributions")]
        public IActionResult Attribs()
        {
            return View("Views/Docs/Attributions.cshtml");
        }

        [Route("/us")]
        public IActionResult Us()
        {
            return View("Views/Home/Us.cshtml");
        }

        [Route("/personalize")]
        public async Task<IActionResult> Personalize()
        {
            
            List<Theme> ThemeDD;
            ThemeDD = new List<Theme>();
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("SELECT a.*,u.FirstName as CuratorName from TblThemeMaster a,TblUserProfile u where a.CuratorId = u.Id and a.IsActive = 1 order by Id", connection);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                ThemeDD.Add(new Theme
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    String = reader.GetString(reader.GetOrdinal("String")),
                    FontLink = reader.GetString(reader.GetOrdinal("FontLink")),
                    CuratorId = reader.GetInt32(reader.GetOrdinal("CuratorId")),
                    CuratorName = reader.GetString(reader.GetOrdinal("CuratorName")),
                    CoverImage = reader.GetString(reader.GetOrdinal("CoverImage")),
                    PrimaryCol = reader.GetString(reader.GetOrdinal("PrimaryCol")),
                });
            }
            await reader.CloseAsync();
            return View(ThemeDD);
        }
    }
}