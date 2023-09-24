using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Text.RegularExpressions;
using theCoffeeroom.Models.Domain;
using theCoffeeroom.Services.Helpers;

namespace theCoffeeroom.Api
{
    [ApiController]
    public class MemberController : Controller
    {
        [HttpGet]
        [Route("/api/members/getmembers/{Classify?}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GetMembers(string Classify)
        {
           
            try
            {
                List<UserProfile> members = new();
                string sql;
                string connectionString = ConfigHelper.NewConnectionString;
                using (SqlConnection connection = new(connectionString))
                {
                    await connection.OpenAsync();
                 

                    if (Classify == "all")
                    {
                        //sql = "SELECT TblUserProfile.Id,TblUserProfile.FirstName,TblUserProfile.LastName,TblUserProfile.UserName," +
                        //    "STRING_AGG(TblAwardMaster.Titile, ', ') WITHIN GROUP (ORDER BY TblAwardMaster.Titile) AS Badges " +
                        //    "FROM TblUserProfile JOIN TblUserAwards ON TblUserProfile.Id = TblUserAwards.UserId " +
                        //    "JOIN TblAwardMaster ON TblUserAwards.AwardId = TblAwardMaster.Id " +
                        //    "GROUP BY TblUserProfile.Id, TblUserProfile.FirstName,TblUserProfile.LastName,TblUserProfile.UserName";
                        sql = "WITH BadgeConcatenation AS" +
                                "(" +
                                    "SELECT TblUserProfile.Id," +
                                    "       TblUserProfile.FirstName," +
                                    "       TblUserProfile.LastName," +
                                    "       TblUserProfile.UserName," +
                                    "       TblUserProfile.AvatarId, " +
                                    "STRING_AGG(TblAwardMaster.Titile, ', ') " +
                                    "WITHIN GROUP " +
                                    "(ORDER BY TblAwardMaster.Titile) AS Badges  " +
                                    "FROM TblUserProfile  " +
                                    "JOIN TblUserAwards " +
                                    "ON TblUserProfile.Id = TblUserAwards.UserId  " +
                                    "JOIN TblAwardMaster " +
                                    "ON TblUserAwards.AwardId = TblAwardMaster.Id  " +
                                    "GROUP BY TblUserProfile.Id, TblUserProfile.FirstName, TblUserProfile.LastName, TblUserProfile.UserName, TblUserProfile.AvatarId " +
                                ") " +
                                    "SELECT  BC.Id,  BC.FirstName,  BC.LastName,  BC.UserName,  BC.AvatarId,  BC.Badges,  AM.Image " +
                                    "FROM  BadgeConcatenation BC " +
                                    "JOIN  TblAvatarMaster AM ON BC.AvatarId = AM.Id ";
                    }
                    else
                    {
                        
                        sql = "WITH BadgeConcatenation AS" +
                                    "(" +
                                        "SELECT TblUserProfile.Id," +
                                        "       TblUserProfile.FirstName," +
                                        "       TblUserProfile.LastName," +
                                        "       TblUserProfile.UserName," +
                                        "       TblUserProfile.AvatarId, " +
                                        "STRING_AGG(TblAwardMaster.Titile, ', ') " +
                                        "WITHIN GROUP " +
                                        "(ORDER BY TblAwardMaster.Titile) AS Badges  " +
                                        "FROM TblUserProfile  " +
                                        "JOIN TblUserAwards " +
                                        "ON TblUserProfile.Id = TblUserAwards.UserId  " +
                                        "JOIN TblAwardMaster " +
                                        "ON TblUserAwards.AwardId = TblAwardMaster.Id  " +
                                        "GROUP BY TblUserProfile.Id, TblUserProfile.FirstName, TblUserProfile.LastName, TblUserProfile.UserName, TblUserProfile.AvatarId " +
                                    ") " +
                                        "SELECT  BC.Id,  BC.FirstName,  BC.LastName,  BC.UserName,  BC.AvatarId,  BC.Badges,  AM.Image " +
                                        "FROM  BadgeConcatenation BC " +
                                        "JOIN  TblAvatarMaster AM ON BC.AvatarId = AM.Id " +
                                        "WHERE Badges like '%"+Classify+"%'";
                    }
                    
                    using SqlCommand command = new(sql, connection);
                    using SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (await dataReader.ReadAsync())
                    {
                        UserProfile member = new()
                        {
                            Id = (int)dataReader["Id"],
                            FirstName = (string)dataReader["FirstName"],
                            LastName= (string)dataReader["LastName"],
                            UserName= (string)dataReader["UserName"],
                            Badges = (string)dataReader["Badges"],
                            AvatarImg = (string)dataReader["Image"],
                        };
                        members.Add(member);
                    }
                    await connection.CloseAsync();
                }
                return new JsonResult(members);
            }
            catch (Exception ex)
            {

                Log.Error("error in live search: " + ex.Message.ToString());
                return BadRequest("something went wrong");
            }
        }

        [HttpGet]
        [Route("/api/member/getdetails/{UserName}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GetUserData(string UserName)
        {
            UserProfile userProfile = null;
            string connectionString = ConfigHelper.NewConnectionString;
            
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT a.FirstName,a.LastName,a.UserName,a.Role,a.Gender,a.Bio,a.DateJoined,b.Image FROM TblUserProfile a, TblAvatarMaster b WHERE UserName = @username and a.AvatarId = b.Id", connection);
                command.Parameters.AddWithValue("@username", UserName);
                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    userProfile = new UserProfile()
                    {
                        FirstName = reader.GetString(0),
                        LastName = reader.GetString(1),
                        UserName = reader.GetString(2),
                        Role = reader.GetString(3),
                        Gender = reader.GetString(4),
                        Bio = reader.GetString(5),
                        DateElement = reader.GetDateTime(6).ToString("D"),
                        AvatarImg = reader.GetString(7)
                    };
                }

                await reader.CloseAsync();
                await connection.CloseAsync();

                return Json(userProfile);
        }


    }
}
