using theCoffeeroom.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using theCoffeeroom.Models.Domain;
using System.Web;
using System;
using Serilog;
using System.Data;

namespace theCoffeeroom.Controllers.Dedicated
{
    public class BlogThumbz
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlHandle { get; set; }
        public string PostContent { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public string Yr { get; set; }
        public string Locator { get; set; }
        public string PostLikes { get; set; }
        public int Comments { get; set; }
        public DateTime DatePosted { get; set; }
    }
    public class BlogTriggers
    {
        public string Mode { get; set; }
        public string Classifypost { get; set; }
        public string Keypost { get; set; }

    }
    public class BlogCat
    {
        public string Category { get; set; }
        public string BlogNum { get; set; }

    }
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class BlogController : Controller
    {
        readonly string connectionString = ConfigHelper.NewConnectionString;
        [HttpGet]
        [Route("api/blogs/{mode}/{classify}/{key}")]
        public async Task<JsonResult> GetBlogs(string mode, string classify, string key)
        {
            mode = "0";
            List<BlogThumbz> thumbs = new();
            _ = new List<BlogThumbz>();
            if (mode != "n")
            {
                
                using SqlConnection connection = new(connectionString);
                await connection.OpenAsync();
                string sql = "";
                if (classify != "na" && key != "na")
                {
                    if (classify == "category")
                    {
                        sql = "SELECT m.Id, m.Title,m.Description,m.UrlHandle,m.DatePosted,m.Tags,YEAR(m.DatePosted) AS Year,c.Title AS Category,c.Locator,COUNT(bc.Id) AS Comments FROM TblBlogMaster m " +
                              "LEFT JOIN TblBlogComment bc ON m.Id = bc.PostId " +
                              "JOIN TblBlogCategory c ON m.CategoryId = c.Id " +
                              "WHERE c.Locator = '" + key + "' and m.IsActive = 1" +
                              "GROUP BY m.Id, m.Title,m.Description,m.UrlHandle, m.DatePosted,m.Tags,c.Title,c.Locator " +
                              "ORDER BY Id OFFSET " + mode + " " +
                              "ROWS FETCH NEXT 2 ROWS ONLY";
                    }
                    else if (classify == "year")
                    {
                        sql = "SELECT m.Id, m.Title,m.Description,m.UrlHandle,m.DatePosted,m.Tags,YEAR(m.DatePosted) AS Year,c.Title AS Category,c.Locator,COUNT(bc.Id) AS Comments FROM TblBlogMaster m " +
                             "LEFT JOIN TblBlogComment bc ON m.Id = bc.PostId " +
                             "JOIN TblBlogCategory c ON m.CategoryId = c.Id " +
                             "WHERE m.CategoryId = c.Id and YEAR(m.DatePosted) = '" + key + "' AND m.IsActive = 1" +
                             "GROUP BY m.Id, m.Title,m.Description,m.UrlHandle, m.DatePosted,m.Tags,c.Title,c.Locator " +
                             "ORDER BY Id OFFSET " + mode + " " +
                             "ROWS FETCH NEXT 2 ROWS ONLY";

                    }
                    else if (classify == "tag")
                    {
                        sql = "SELECT m.Id, m.Title,m.Description,m.UrlHandle,m.DatePosted,m.Tags,YEAR(m.DatePosted) AS Year,c.Title AS Category,c.Locator,COUNT(bc.Id) AS Comments FROM TblBlogMaster m " +
                             "LEFT JOIN TblBlogComment bc ON m.Id = bc.PostId " +
                             "JOIN TblBlogCategory c ON m.CategoryId = c.Id " +
                             "WHERE m.CategoryId = c.Id and Tags like '%" + key + "%' AND m.IsActive = 1" +
                             "GROUP BY m.Id, m.Title,m.Description,m.UrlHandle, m.DatePosted,m.Tags,c.Title,c.Locator " +
                             "ORDER BY Id OFFSET " + mode + " " +
                             "ROWS FETCH NEXT 2 ROWS ONLY";
                    }

                    else if (classify == "search")
                    {
                        sql = "SELECT m.Id, m.Title,m.Description,m.UrlHandle,m.DatePosted,m.Tags,YEAR(m.DatePosted) AS Year,c.Title AS Category,c.Locator,COUNT(bc.Id) AS Comments FROM TblBlogMaster m " +
                             "LEFT JOIN TblBlogComment bc ON m.Id = bc.PostId " +
                             "JOIN TblBlogCategory c ON m.CategoryId = c.Id " +
                             "WHERE m.CategoryId = c.Id and (m.Title like '%" + key + "%' OR m.Description like '%"+key+ "%'  OR m.Tags like '%"+key+"%' ) AND m.IsActive = 1" +
                             "GROUP BY m.Id, m.Title,m.Description,m.UrlHandle, m.DatePosted,m.Tags,c.Title,c.Locator " +
                             "ORDER BY Id OFFSET " + mode + " " +
                             "ROWS FETCH NEXT 2 ROWS ONLY";
                    }
                }
                else
                {
                    sql = "SELECT m.Id, m.Title,m.Description,m.UrlHandle,m.DatePosted,m.Tags,YEAR(m.DatePosted) AS Year,c.Title AS Category,c.Locator,COUNT(bc.Id) AS Comments FROM TblBlogMaster m " +
                            "LEFT JOIN TblBlogComment bc ON m.Id = bc.PostId " +
                            "JOIN TblBlogCategory c ON m.CategoryId = c.Id " +
                            "WHERE m.CategoryId = c.Id  AND m.IsActive = 1" +
                            "GROUP BY m.Id, m.Title,m.Description,m.UrlHandle, m.DatePosted,m.Tags,c.Title,c.Locator " +
                            "ORDER BY Id OFFSET " + mode + " " +
                            "ROWS FETCH NEXT 2 ROWS ONLY";
                }
                using SqlCommand command = new(sql, connection);
                using SqlDataReader dataReader = await command.ExecuteReaderAsync();

                if (dataReader.HasRows)
                {
                    while (await dataReader.ReadAsync())
                    {
                        thumbs.Add(new BlogThumbz
                        {
                            Title = dataReader.GetString(1),
                            Description = dataReader.GetString(2),
                            UrlHandle = dataReader.GetString(3),
                            DatePosted = dataReader.GetDateTime(4),
                            Category = dataReader.GetString(7),
                            Locator = dataReader.GetString(8),
                            Comments = dataReader.GetInt32(9)
                        });
                    }
                }
                await connection.CloseAsync();
            }
            return new JsonResult(thumbs);
        }


        [HttpGet]
        [Route("api/blog/{Slug}/authors")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> LoadAuthors(string Slug)
        {
            List<object> data = new();
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT ba.AuthorId,u.UserName,u.Bio,u.FirstName,u.LastName,avt.Image FROM TblBlogMaster AS b " +
                                        "JOIN TblBlogAuthor AS ba ON ba.BlogId = b.Id " +
                                        "JOIN TblUserProfile AS u ON ba.AuthorId = u.Id " +
                                        "JOIN TblAvatarMaster AS avt ON u.AvatarId = avt.Id where b.UrlHandle = @UrlHandle ", connection);
            //added scalar vars
            command.Parameters.AddWithValue("@UrlHandle", Slug);
            var reader =await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var row = new
                {
                    userId = reader.GetInt32(0),
                    userName = reader.GetString(1),
                    userBio = reader.GetString(2),
                    firstName = reader.GetString(3),
                    lastName = reader.GetString(4),
                    avatarImage = reader.GetString(5),
                };

                data.Add(row);
            }
            await reader.CloseAsync();
            await connection.CloseAsync();
            return new JsonResult(data);
        }


        [HttpPost]
        [Route("api/blog/comment/add")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddComment([FromBody] BlogComment blogComment)
        {

            string userid = "", blogid = "";


            if (blogComment.Comment != null && blogComment.Slug != null)
            {

                try
                {
                    string encodedcomment = HttpUtility.HtmlEncode(blogComment.Comment.ToString().Trim());
                    if (encodedcomment.Length >= 3)
                    {
                        using var connection = new SqlConnection(connectionString);
                        await connection.OpenAsync();
                        var command = new SqlCommand("SELECT Id FROM TblBlogMaster WHERE UrlHandle = @urlhandle", connection);
                        command.Parameters.AddWithValue("@urlhandle", blogComment.Slug);
                        var reader = await command.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            blogid = reader.GetInt32(0).ToString();
                        }
                        reader.Close();
                        command = new SqlCommand("SELECT Id FROM TblUserProfile WHERE UserName = @username", connection);
                        command.Parameters.AddWithValue("@username", HttpContext.Session.GetString("username").ToString());
                        reader = await command.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            userid = reader.GetInt32(0).ToString();
                        }
                        reader.Close();
                        SqlCommand maxIdCommand = new("SELECT ISNULL(MAX(Id), 0) + 1 FROM TblBlogComment", connection);
                        int newId = Convert.ToInt32(maxIdCommand.ExecuteScalar());

                        command = new SqlCommand("insert into TblBlogComment(Id,Comment,UserId,PostId,IsActive,DatePosted) values(" + newId + ",'" + encodedcomment + "'," + userid + "," + blogid + ",1,@dateposted)", connection);
                        command.Parameters.Add("@dateposted", SqlDbType.DateTime).Value = DateTime.Now;
                        await command.ExecuteNonQueryAsync();
                        return Ok("Comment added");
                    }
                    else
                    {
                        return BadRequest("Comment too short");
                    }

                }
                catch (Exception ex)
                {
                    Log.Information("error in adding comment by" + HttpContext.Session.GetString("username") + "on a blog :" + ex.Message.ToString());
                    return BadRequest("Something went wrong");
                }
            }
            else
            {
                return BadRequest("Something went wrong");
            }
    
        }


        [HttpPost]
        [Route("api/blog/comments/load")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> LoadComments([FromBody] BlogComment blogComment)
        {

            Dictionary<int, dynamic> comments = new();
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            var command = new SqlCommand(@"SELECT
						  c.Id,
						  c.Comment,
						  c.UserId,
						  u.FirstName,
						  u.LastName,
						  u.UserName as CommentUserName,
						  u.Id,
						  LEFT(c.DatePosted, 12),
						  a.Image,
						  r.Id AS ReplyId,
						  r.Reply AS ReplyComment,						  
						LEFT(r.DatePosted, 12),
						  u2.Id AS ReplyUserId,
						  u2.FirstName AS ReplyFirstName,
						  u2.LastName AS ReplyLastName,
						  a2.Image AS ReplyImage,
						u2.UserName AS ReplyUserName
						FROM
						  TblBlogComment c
						  JOIN TblUserProfile u ON c.UserId = u.Id
						  JOIN TblAvatarMaster a ON u.AvatarId = a.Id
						  LEFT JOIN TblBlogReply r ON c.Id = r.CommentId
                          LEFT JOIN TblBlogMaster bm ON bm.Id = c.PostId
						  LEFT JOIN TblUserProfile u2 ON r.UserId = u2.Id
						  LEFT JOIN TblAvatarMaster a2 ON u2.AvatarId = a2.Id
            where bm.UrlHandle = @posturl
						ORDER BY
						  c.DatePosted;
						", connection);
            command.Parameters.AddWithValue("@posturl", blogComment.Slug);
            var reader = await command.ExecuteReaderAsync();
            string user = "";
            bool editable = false, replyeditable = false;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (HttpContext.Session.GetString("username") != null)
                    {
                        user = "yes";
                        if (reader.GetString(5) == HttpContext.Session.GetString("username").ToString())
                        {
                            editable = true;
                        }
                        else
                        {
                            editable = false;
                        }
                        try
                        {
                            if (reader.GetString(16) == HttpContext.Session.GetString("username").ToString())
                            {
                                replyeditable = true;
                            }
                            else
                            {
                                replyeditable = false;
                            }
                        }
                        catch
                        {
                            replyeditable = false;
                        }
                    }
                    else
                    {
                        user = "no";
                    }
                    var commentId = reader.GetInt32(0);
                    if (!comments.ContainsKey(commentId))
                    {
                        var comment = new
                        {
                            id = commentId,
                            edit = editable,
                            user,
                            fullname = reader.GetString(3) + " " + reader.GetString(4),
                            userid = reader.GetInt32(2),
                            username = reader.GetString(5),
                            comment = HttpUtility.HtmlDecode(reader.GetString(1)),
                            date = reader.GetString(7),
                            avatar = reader.GetString(8),
                            replies = new List<object>()
                        };

                        comments.Add(commentId, comment);
                    }

                    if (!reader.IsDBNull(9))
                    {
                        var reply = new
                        {
                            replyEdit = replyeditable,
                            user,
                            replyId = reader.GetInt32(9),
                            replyComment = HttpUtility.HtmlDecode(reader.GetString(10)),
                            replyUserId = reader.GetInt32(12),
                            replyDate = reader.GetString(11),
                            //replyFirstName = reader.GetString(13),
                            //replyLastName = reader.GetString(14),
                            replyFullName = reader.GetString(13) + " " + reader.GetString(14),
                            replyAvatar = reader.GetString(15)
                        };

                        comments[commentId].replies.Add(reply);
                    }
                }
            }
            else
            {
                //return StatusCode(404);
                string errorMessage = "error";
                return BadRequest(new { error = errorMessage }); // HTTP 400 Bad Request
            }

            await reader.CloseAsync();
            await connection.CloseAsync();
            return new JsonResult(comments.Values);

        }


        [HttpPost]
        [Route("api/blog/comment/edit")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> EditComment([FromBody] BlogComment blogComment)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                var Userdet = HttpContext.Session.GetString("user_id").ToString();
                try
                {

                    using var connection = new SqlConnection(connectionString);
                    
                    await connection.OpenAsync();
                    var sql = "UPDATE TblBlogComment SET Comment = @Commentval WHERE Id = @Idval AND UserId = @UserId";
                    var command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Commentval", HttpUtility.HtmlEncode(blogComment.Comment));
                    command.Parameters.AddWithValue("@Idval", blogComment.Id);
                    command.Parameters.AddWithValue("@UserId", Userdet);
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    return Ok("changes saved");
                }
                catch (Exception ex)
                {
                    Log.Error("error editing comment by user " + Userdet + "message:" + ex.Message.ToString());
                    return BadRequest("Something went wrong");
                }
            }
            else
            {
                return BadRequest("Access denied");
            }
            
        }


        [HttpPost]
        [Route("api/blog/comment/delete")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteComment([FromBody] BlogComment blogComment)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                using var transaction = connection.BeginTransaction();
                try
                {
                    // Perform multiple database operations within the transaction
                    using var command1 = connection.CreateCommand();
                    command1.Transaction = transaction;
                    command1.CommandText = "DELETE FROM TblBlogComment WHERE Id = @id and UserId = @user_id";
                    command1.Parameters.AddWithValue("@id", blogComment.Id);
                    command1.Parameters.AddWithValue("@user_id", HttpContext.Session.GetString("user_id"));

                    await command1.ExecuteNonQueryAsync();

                    using var command2 = connection.CreateCommand();
                    command2.Transaction = transaction;
                    command2.CommandText = "DELETE FROM TblBlogReply WHERE CommentId = @id";
                    command2.Parameters.AddWithValue("@id", blogComment.Id);
                    command2.Parameters.AddWithValue("@user_id", HttpContext.Session.GetString("user_id"));
                    await command2.ExecuteNonQueryAsync();
                    transaction.Commit();
                    return Ok("Commend deleted");

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Log.Error("error deleting comment:" + ex.Message.ToString());
                    return BadRequest("Something went wrong");
                }
            }
            else
            {
                return BadRequest("Access Denied");
            }
           
        }


        [HttpPost]
        [Route("api/blog/reply/add")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddReply([FromBody] BlogReply blogReply)
        {
            string userid = "";
            string encodedreply = HttpUtility.HtmlEncode(blogReply.ReplyText.ToString().Trim());
            if (blogReply.Slug != null)
            {
                try
                {
                    if (encodedreply.Trim() != "")
                    {
                        using var connection = new SqlConnection(connectionString);
                        await connection.OpenAsync();
                        var command = new SqlCommand("SELECT Id FROM TblUserProfile WHERE UserName = @username", connection);
                        command.Parameters.AddWithValue("@username", HttpContext.Session.GetString("username").ToString());
                        var reader = await command.ExecuteReaderAsync();
                        if (await reader.ReadAsync())
                        {
                            userid = reader.GetInt32(0).ToString();
                        }
                        reader.Close();
                        SqlCommand maxIdCommand = new("SELECT ISNULL(MAX(Id), 0) + 1 FROM TblBlogReply", connection);
                        int newId = Convert.ToInt32(maxIdCommand.ExecuteScalar());

                        command = new SqlCommand("insert into TblBlogReply(Id,CommentId,UserId,Reply,IsActive,DatePosted) values(" + newId + ",'" + blogReply.CommentId + "','" + userid + "','" + encodedreply + "',1,@dateposted)", connection);
                        command.Parameters.Add("@dateposted", SqlDbType.DateTime).Value = DateTime.Now;
                        await command.ExecuteNonQueryAsync();
                        return Ok("reply added");
                    }
                    else
                    {

                        return BadRequest("Reply too short");
                    }

                }
                catch (Exception ex)
                {
                    Log.Information("error in adding reply by" + HttpContext.Session.GetString("username") + "on a blog :" + ex.Message.ToString());
                    return BadRequest("Something went wrong");
                }
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }


        [HttpPost]
        [Route("api/blog/reply/edit")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> EditrReply([FromBody] BlogReply blogReply)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                var Userdet = HttpContext.Session.GetString("user_id").ToString();
                try
                {

                    using var connection = new SqlConnection(connectionString);
                    await connection.OpenAsync();
                    var sql = "UPDATE TblBlogReply SET Reply = @Replyval WHERE Id = @Idval AND UserId = @UserId ";
                    var command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Replyval", HttpUtility.HtmlEncode(blogReply.Reply));
                    command.Parameters.AddWithValue("@Idval", blogReply.ReplyId);
                    command.Parameters.AddWithValue("@UserId", HttpContext.Session.GetString("user_id").ToString());
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    return Ok("changes saved");
                }
                catch (Exception ex)
                {
                    Log.Error("error editing reply by user " + Userdet + "message:" + ex.Message.ToString());
                    return BadRequest("Something went wrong");
                }
            }
            else
            {
                return BadRequest("Access denied");
            }

        }


        [HttpPost]
        [Route("api/blog/reply/delete")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteReply([FromBody] BlogReply blogReply)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                try
                {
                    using var connection = new SqlConnection(connectionString);
                    await connection.OpenAsync();
                    using var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM TblBlogReply WHERE Id = @id and UserId = @user_id";
                    command.Parameters.AddWithValue("@id", blogReply.ReplyId);
                    command.Parameters.AddWithValue("@user_id", HttpContext.Session.GetString("user_id"));
                    await command.ExecuteNonQueryAsync();
                    return Ok("Commend deleted");

                }
                catch
                {
                    return BadRequest("Something went wrong");
                }
               
            }
            else
            {
                return BadRequest("Access Denied");
            }

        }

    }
}
