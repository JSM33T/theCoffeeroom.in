using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using theCoffeeroom.Models.Frame;
using theCoffeeroom.Services.Helpers;

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


        //SEO-prerender
        [HttpGet]
        [Route("/blog/{Year}/{Slug}")]
        public IActionResult Blogs(string Year,string Slug)
        {
            string connectionString = ConfigHelper.NewConnectionString;
            BlogLoad blogLoad = null;
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("SELECT a.Id, a.Tags, a.Title, a.UrlHandle, COUNT(b.blogid) AS LikeCount FROM TblBlogMaster a LEFT JOIN TblBlogLike b ON a.Id = b.blogid WHERE a.UrlHandle = '"+Slug+"' GROUP BY a.Id, a.Tags, a.Title, a.UrlHandle; ", connection);
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
                Year = Year,
                Likes = reader["LikeCount"].ToString(),
                };
                
            }
            reader.Close();
            return View("Views/Blogs/Frame.cshtml", blogLoad);
        }


    }
}
