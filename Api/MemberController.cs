using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Text.RegularExpressions;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;

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

      
    }
}
