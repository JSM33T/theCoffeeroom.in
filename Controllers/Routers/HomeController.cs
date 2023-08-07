﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Controllers.Routers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index(string level)
        {
            return View(level);
        }

        [Route("/changelog")]
        public IActionResult Changelog()
        {
            return View();
        }

        [Route("/faq")]
        public IActionResult Faq()
        {
            return View();
        }

        [Route("/personalize")]
        public async Task<IActionResult> Personalize()
        {
            string connectionString = ConfigHelper.NewConnectionString;
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}