using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace theCoffeeroom.Controllers.Dedicated
{
    public class AdminController : Controller
    {

        [Route("/admin")]
        public IActionResult Index()
        {
            return View("Views/Admin/Index.cshtml");
        }

        [Route("/admin/logs")]
        public IActionResult LogList()
        {
            if (HttpContext.Session.GetString("role") != null)
            {
                if (HttpContext.Session.GetString("role") == "admin")
                {
                    string logsDirectory = "Logs"; // Provide the actual path

                    var logFiles = Directory.GetFiles(logsDirectory, "*.txt")
                                           .Select(filePath => new FileInfo(filePath))
                                           .OrderByDescending(fileInfo => fileInfo.CreationTime)
                                           .ToList();

                    var logFileNames = logFiles.Select(fileInfo => fileInfo.Name).ToList();

                    return View("Views/Admin/Logs/List.cshtml", logFileNames);
                }
                else
                {
                    return View("Views/Home/AccessDenied.cshtml");
                }
            }
            else
            {
                return View("Views/Home/AccessDenied.cshtml");
            }
           
        }

        [Route("/admin/log/{filename}")]
        public IActionResult ViewLogFile(string fileName)
        {
            if (HttpContext.Session.GetString("role") != null)
            {
                if (HttpContext.Session.GetString("role") == "admin")
                {
                     string logsDirectory = "Logs"; 
                    string filePath = Path.Combine(logsDirectory,"coffeelog" +fileName + ".txt");

                    if (!System.IO.File.Exists(filePath))
                    {
                        return NotFound();
                    }

                    List<string> fileLines = System.IO.File.ReadAllLines(filePath).ToList();

                    ViewData["FileName"] = fileName;
                    ViewData["FileLines"] = fileLines;

                    return View("Views/Admin/Logs/View.cshtml");
                }
                else
                {
                    return View("Views/Home/AccessDenied.cshtml");
                }
            }
            else
            {
                return View("Views/Home/AccessDenied.cshtml");
            }
        }

    


    }
}
