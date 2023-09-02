using Microsoft.AspNetCore.Mvc;

namespace theCoffeeroom.Controllers.Dedicated
{
    public class FilesController : Controller
    {
        //[Route("shell")]
        //public IActionResult Home()
        //{
        //    string fallbackFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "shell.html");

        //    if (System.IO.File.Exists(fallbackFilePath))
        //    {
        //        return PhysicalFile(fallbackFilePath, "text/html");
        //    }
        //    else
        //    {
        //        return NotFound(); 
        //    }
        //}

        [Route("shell")]
        public IActionResult Home()
        {
            return View("Views/Home/Shell.cshtml");
        }


        [Route("browser/{Subdir1?}/{Subdir2?}/{Subdir3?}/{Subdir4?}/{Subdir5?}")]
        public IActionResult Something(string Subdir1,string Subdir2,string Subdir3,string Subdir4,string Subdir5)
        {
            string targetDirectory = "";
            if (Subdir1 == null)
            {
                targetDirectory = "wwwroot/content/";
            }
            else if (Subdir2 == null)
            {
                targetDirectory = "wwwroot/content/" + Subdir1.ToLower();
            }
            else if (Subdir3 == null)
            {
                targetDirectory = "wwwroot/content/" + Subdir1.ToLower() + "/" + Subdir2.ToLower();
            }
            else if (Subdir4 == null)
            {
                targetDirectory = "wwwroot/content/" + Subdir1.ToLower() + "/" + Subdir2.ToLower() + "/" + Subdir3.ToLower();
            }
            else if(Subdir5 == null)
            {
                targetDirectory = "wwwroot/content/" + Subdir1.ToLower() + "/" + Subdir2.ToLower() + "/" + Subdir3.ToLower() + "/" + Subdir4.ToLower();
            }
             else
            {
                targetDirectory = "wwwroot/content/" + Subdir1.ToLower() + "/" + Subdir2.ToLower() + "/" + Subdir3.ToLower() + "/" + Subdir4.ToLower() + "/"  + Subdir5.ToLower();
            }


            var items = Directory.GetFileSystemEntries(targetDirectory)
                                         .Select(path =>
                                         {
                                             var info = new DirectoryInfo(path);
                                             return new
                                             {
                                                 Name = info.Name,
                                                 IsDirectory = info.Attributes.HasFlag(FileAttributes.Directory),
                                                 CreatedAt = info.CreationTime,
                                                 Path = path,
                                             };
                                         })
                                         .OrderByDescending(item => item.CreatedAt)
                                         .ToList();

                    return Json(items); // Return JSON response with directories and files
               
           
        }


    }
}
