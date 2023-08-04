using theCoffeeroom.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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
    public class BlogController : Controller
    {
        [HttpGet("api/blogs/{mode}/{classify}/{key}")]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> GetBlogs(string mode, string classify, string key)
        {
            mode = "0";
            List<BlogThumbz> thumbs = new();
            _ = new List<BlogThumbz>();
            if (mode != "n")
            {
                string connectionString = ConfigHelper.NewConnectionString;
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
                             "WHERE m.CategoryId = c.Id and m.Title like '%" + key + "%' AND m.IsActive = 1" +
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
    }
}
