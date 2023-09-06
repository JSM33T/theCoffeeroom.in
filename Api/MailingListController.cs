using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Api
{
    public class MailingListController : Controller
    {
        public string connectionString = ConfigHelper.NewConnectionString;

        [HttpPost]
        [Route("api/newsletter")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMailToLetter([FromBody] Mail mail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using SqlConnection connection = new(connectionString);
                    await connection.OpenAsync();

                    string checkEmailQuery = "SELECT COUNT(*) FROM TblMailingList WHERE Email = @Email";
                    SqlCommand checkEmailCmd = new(checkEmailQuery, connection);
                    checkEmailCmd.Parameters.AddWithValue("@Email", mail.EMailId);
                    int emailCount = (int)await checkEmailCmd.ExecuteScalarAsync();
                    try
                    {
                        if (emailCount == 0)
                        {
                            string getMaxIdQuery = "SELECT MAX(Id) FROM TblMailingList";
                            SqlCommand getMaxIdCmd = new(getMaxIdQuery, connection);
                            int maxId = (int)await getMaxIdCmd.ExecuteScalarAsync();
                            int newId = maxId + 1;

                            string addEmailQuery = "INSERT INTO TblMailingList (Id, Email,Origin,DateAdded) VALUES (@id, @email,@origin,@dateadded)";
                            SqlCommand addEmailCmd = new(addEmailQuery, connection);
                            addEmailCmd.Parameters.AddWithValue("id", newId);
                            addEmailCmd.Parameters.AddWithValue("@email", mail.EMailId);
                            addEmailCmd.Parameters.AddWithValue("@origin", mail.Origin);
                            addEmailCmd.Parameters.AddWithValue("@dateadded", DateTime.Now);
                            await addEmailCmd.ExecuteNonQueryAsync();
                            Log.Information("mail added to newsletter:" + mail.EMailId);

                            return Ok("Email added to the list");

                        }
                        else
                        {
                            return BadRequest("Email already exists");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("email da repo error" + ex.Message.ToString());
                        throw new Exception("something went wrong");
                    }
                }
                else
                {
                    return BadRequest("Invalid email");
                }

            }
            catch (Exception ex)
            {
                Log.Error("error while email submission:" + ex.Message.ToString());
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
