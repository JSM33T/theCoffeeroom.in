using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;
using theCoffeeroom.Models.Domain;
using theCoffeeroom.Models.Frame;
using theCoffeeroom.Services.Helpers;

namespace theCoffeeroom.Api
{

    [ApiController]
    public class ProfileController : ControllerBase
    {
        
        [HttpGet("api/profile/userbadges")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GetBadges()
        {

            string Connectionstring = ConfigHelper.NewConnectionString;
            List<UserAwards> badges = new();

            using SqlConnection conn = new(Connectionstring);
            await conn.OpenAsync();
            SqlCommand cmd = new("SELECT a.*, m.Titile,m.Description,m.Icon " +
                                    "FROM TblUserAwards a " +
                                    "INNER JOIN TblAwardMaster m " +
                                    "ON m.Id = a.AwardId " +
                                    "WHERE a.UserId = @userid", conn);

            cmd.Parameters.AddWithValue("@userid", HttpContext.Session.GetString("user_id"));

            try
            {
                using SqlDataReader rd = await cmd.ExecuteReaderAsync();
                while (rd.Read())
                {
                    UserAwards badge = new()
                    {
                        Id = rd.GetInt32(rd.GetOrdinal("Id")),
                        Title = rd.GetString(rd.GetOrdinal("Titile")),
                        Description = rd.GetString(rd.GetOrdinal("Description")),
                        Icon = rd.GetString(rd.GetOrdinal("Icon"))
                    };
                    badges.Add(badge);
                }

                return Ok(badges);
            }
            catch (SqlException ex)
            {
                Log.Error("Error fetching awards " + ex.Message.ToString());
                return BadRequest("Something went wrong:");
                
            }
            catch (Exception exg)
            {
                Log.Error("Error fetching awards " + exg.Message.ToString());
                return BadRequest("Something went wrong" );
            }


            //string Connectionstring = ConfigHelper.NewConnectionString;
            //List<UserAwards> badges = new();
            //Log.Error(Connectionstring.ToString());

            //using SqlConnection conn = new(Connectionstring);
            //await conn.OpenAsync();
            //SqlCommand cmd = new()
            //{
            //    CommandText = "SELECT a.* ,m.Titile   from TblUserAwards a   inner join TblAwardMaster m on m.Id = a.AwardId   where a.UserId = @userid"
            //};
            //cmd.Parameters.AddWithValue("@userid", 1);
            //try {
            //    using SqlDataReader rd = await cmd.ExecuteReaderAsync();
            //    while (rd.Read())
            //    {
            //        UserAwards badge = new()
            //        {
            //            Id = rd.GetInt32(rd.GetOrdinal("Id")),
            //            Title = rd.GetString(rd.GetOrdinal("Titile")),         
            //            Description = rd.GetString(rd.GetOrdinal("Description")),
            //        };
            //        badges.Add(badge);

            //    }  
            //    return Ok(badges);
            //}
            //catch(SqlException ex) {
            //    return BadRequest("something went wrong" + ex.Message.ToString());
            //}



        }
    }
}
