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
    public class HomeController : ControllerBase
    {
        
        [HttpGet("/api/topblogs/get")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GetTopBlogs()
        {
            try
            {
                List<BlogThumbz> entries = new();
                string sql;
                string connectionString = ConfigHelper.NewConnectionString;
                using (SqlConnection connection = new(connectionString))
                {
                    await connection.OpenAsync();
                    //sql = "SELECT TOP(3) bm.ID, bm.Title,bm.Description,bm.UrlHandle, bm.DatePosted, COUNT(bc.Id) AS CommentCount FROM TblBlogMaster bm " +
                    //        "LEFT JOIN TblBlogComment bc ON bm.ID = bc.PostId " +
                    //        "WHERE bm.IsActive = 1 " +
                    //        "GROUP BY bm.ID, bm.Title,bm.Description,bm.UrlHandle, bm.DatePosted " +
                    //        "ORDER BY bm.DatePosted DESC, CommentCount DESC";

                    sql = $@"
                            SELECT 
                                bm.ID, 
                                bm.Title, 
                                bm.Description, 
                                bm.UrlHandle, 
                                bm.DatePosted, 
                                COALESCE(comment_counts.CommentCount, 0) AS CommentCount,
                                COALESCE(like_counts.LikeCount, 0) AS LikeCount
                            FROM TblBlogMaster bm
                            LEFT JOIN (
                                SELECT 
                                    PostId, 
                                    COUNT(Id) AS CommentCount
                                FROM TblBlogComment
                                GROUP BY PostId
                            ) AS comment_counts ON bm.ID = comment_counts.PostId
                            LEFT JOIN (
                                SELECT 
                                    BlogId, 
                                    COUNT(Id) AS LikeCount
                                FROM TblBlogLike
                                GROUP BY BlogId
                            ) AS like_counts ON bm.ID = like_counts.BlogId
                            WHERE bm.IsActive = 1
                            ORDER BY bm.DatePosted DESC
                            ";

                    using SqlCommand command = new(sql, connection);
                    using SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (await dataReader.ReadAsync())
                    {
                        BlogThumbz entry = new()
                        {
                            Id = (int)dataReader["Id"], 
                            Title = (string)dataReader["Title"],
                            Description = (string)dataReader["Description"],
                            DatePosted = (DateTime)dataReader["DatePosted"],
                            DateFormatted = DateTimeFormats.FormatDateOrRelative((DateTime)dataReader["DatePosted"]),
                            UrlHandle = (string)dataReader["UrlHandle"],
                            Comments = (int)dataReader["CommentCount"],
                            Likes = (int)dataReader["LikeCount"]
                        };
                        entries.Add(entry);
                    }
                    await connection.CloseAsync();
                }
                return new JsonResult(entries);
            }
            catch (Exception ex)
            {

                Log.Error("error in get top blogs: " + ex.Message.ToString());
                return BadRequest("something went wrong");
            }
        }

           [HttpGet("/api/featuredblog/get")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GetFeaturedBlog()
        {
            try
            {
                List<BlogThumbz> entries = new();
                string sql;
                string connectionString = ConfigHelper.NewConnectionString;
                using (SqlConnection connection = new(connectionString))
                {
                    await connection.OpenAsync();
                    sql = "SELECT TOP(1) bm.ID, bm.Title,bm.Description,bm.UrlHandle, bm.DatePosted, COUNT(bc.Id) AS CommentCount FROM TblBlogMaster bm " +
                            "LEFT JOIN TblBlogComment bc ON bm.ID = bc.PostId " +
                            "WHERE bm.IsActive = 1" +
                            "GROUP BY bm.ID, bm.Title,bm.Description,bm.UrlHandle, bm.DatePosted " +
                            "ORDER BY bm.DatePosted DESC, CommentCount DESC";

                    using SqlCommand command = new(sql, connection);
                    using SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (await dataReader.ReadAsync())
                    {
                        BlogThumbz entry = new()
                        {
                            Id = (int)dataReader["Id"],
                            Title = (string)dataReader["Title"],
                            Description = (string)dataReader["Description"],
                            DatePosted = (DateTime)dataReader["DatePosted"],
                            UrlHandle= (string)dataReader["UrlHandle"],
                            Comments = (int)dataReader["CommentCount"]
                        };
                        entries.Add(entry);
                    }
                    await connection.CloseAsync();
                }
                return new JsonResult(entries);
            }
            catch (Exception ex)
            {

                Log.Error("error in get top blogs: " + ex.Message.ToString());
                return BadRequest("something went wrong");
            }
        }

       
    }
}
