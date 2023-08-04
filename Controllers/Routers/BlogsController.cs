using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;
using theCoffeeroom.Models.Frame;
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
            string connectionString = ConfigHelper.NewConnectionString;
            BlogLoad blogLoad = null;
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("SELECT Id, Tags,Title,UrlHandle FROM TblBlogMaster WHERE UrlHandle = @urlhandle", connection);
            command.Parameters.AddWithValue("@urlhandle", Slug);
            var reader = command.ExecuteReader();
            string tags = string.Empty;

            if (reader.Read())
            {
                blogLoad = new() 
                {
                Id = (int)reader["Id"],
                Tags = reader["Tags"].ToString(),
                Title = reader["Title"].ToString(),
                Slug = reader["UrlHandle"].ToString(),
                Year = Year
                };
                
            }
            reader.Close();
            return View("Views/Blogs/Frame.cshtml", blogLoad);
        }


    }
}
