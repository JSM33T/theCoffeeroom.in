using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Data;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;
using theCoffeeroom.Models.Frame;
using Validators = theCoffeeroom.Core.Helpers.Validators;

namespace theCoffeeroom.Controllers.Dedicated
{
    public class AccountController : Controller
    {

        readonly string connectionString = ConfigHelper.NewConnectionString;

        [HttpPost("/api/account/login")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UserLogin([FromBody] LoginCreds loginCreds)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            try
            {
                using SqlConnection connection = new(connectionString);
                await connection.OpenAsync();
                SqlCommand checkcommand = new("select p.*,a.Image " +
                    "from TblUserProfile p,TblAvatarMaster a " +
                    "where UserName = @username " +
                    "and PassWord =@password " +
                    "and p.IsActive= 1 " +
                    "and p.IsVerified = 1 " +
                    "and p.AvatarId = a.Id", connection);
                checkcommand.Parameters.AddWithValue("@username", loginCreds.UserName.ToLower());
                checkcommand.Parameters.AddWithValue("@password", loginCreds.Password);
                using (var reader = await checkcommand.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        var username = reader.GetString(reader.GetOrdinal("UserName"));
                        var user_id = reader.GetInt32(reader.GetOrdinal("Id"));
                        var firstname = reader.GetString(reader.GetOrdinal("FirstName"));
                        var fullname = reader.GetString(reader.GetOrdinal("FirstName")) + " " + reader.GetString(reader.GetOrdinal("LastName"));
                        var role = reader.GetString(reader.GetOrdinal("Role"));
                        var avatar = reader.GetString(reader.GetOrdinal("Image"));
                        //set session
                        HttpContext.Session.SetString("user_id", user_id.ToString());
                        HttpContext.Session.SetString("username", username);
                        HttpContext.Session.SetString("first_name", firstname);
                        HttpContext.Session.SetString("role", role);
                        HttpContext.Session.SetString("fullname", fullname);
                        HttpContext.Session.SetString("avatar", avatar.ToString());
                        return Ok("logging in...");
                    }
                    else
                    {

                    }
                    {
                        Log.Information("invalid creds by username:" + loginCreds.UserName);
                      
                    }
                }
                await connection.CloseAsync();

                return BadRequest("Invalid Credentials");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString() + " : in login form,user : " + loginCreds.UserName);
                return StatusCode(500, "soomething went wrong");
               
            }
        }

        [HttpPost("/api/account/signup")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UserSignUp([FromBody] UserProfile userProfile)
        {
            string body, subject;
           
            string message = "something working", type = "error";
            if (userProfile.UserName != null && userProfile.Password!= null)
            {
                if (userProfile.FirstName == "")
                {
                    message = "first name is mandatory";
                }
                else if (userProfile.LastName == "")
                {
                    message = "username is mandatory";
                }
                else if (!Validators.IsAlphaNumeric(userProfile.UserName))
                {
                    message = "only alphanumeric characters are allowed";
                }
                else if (userProfile.EMail == "")
                {
                    message = "email is mandatory";
                }
                else if (Validators.IsValidEmail(userProfile.EMail) == false)
                {
                    message = "invalid email format";
                }
                else if (userProfile.Password.Trim() == "")
                {
                    message = "password is mandatory";
                }
                else if (userProfile.Password.Trim().Length <= 6)
                {
                    message = "password should be atlease 6 chars long";
                }
                else
                {
                    try
                    {
                        string FilteredUsername = userProfile.UserName.Trim().ToLower().ToString();
                        using SqlConnection connection = new(connectionString);
                        await connection.OpenAsync();
                        SqlCommand cmd = new("select count(*) from TblUserProfile where UserName = @inputusername or EMail = @inputemail", connection);
                        cmd.Parameters.AddWithValue("@inputusername", FilteredUsername);
                        cmd.Parameters.AddWithValue("@inputemail", userProfile.EMail);
                        var counter = cmd.ExecuteScalar().ToString();
                        if (counter == "0")
                        {
                            string secret = StringProcessors.GenerateRandomString(10);
                            var otp = OTPGenerator.GenerateOTP(secret);
                            subject = "Verify Your Account | TheCoffeeroom";
                            try
                            {

                                body = "<h1>Hey there,</h1>" +
                                        "<p> This is for the verification of your account @TheCoffeeRoom." +
                                        "" + otp + " is your OTP which is valid for 30 minutes </p>." +
                                        "Or alternatively you can click here to verify directly:" +
                                        "<a type=\"button\" href=\"https://thecoffeeroom.in/account/verification/" + FilteredUsername + "/" + otp + "\"><b> VERIFY </b></a>";

                                int stat = Mailer.MailSignup(subject, body, userProfile.EMail.ToString());
                                if (stat == 1)
                                {
                                    try
                                    {
                                        SqlCommand maxIdCommand = new("SELECT ISNULL(MAX(Id), 0) + 1 FROM TblUserProfile", connection);
                                        int newId = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                                        cmd = new("insert into TblUserProfile (Id,FirstName,LastName,EMail,UserName,PassWord,IsActive,IsVerified,OTP,OTPTime,Role,Bio,Gender,Phone,AvatarId,DateJoined) VALUES(@Id,@firstname,@lastname,@email,@username,@password,1,0,@otp,@otptime,'user','','','',1,@datejoined)", connection);
                                        cmd.Parameters.AddWithValue("@Id", newId);
                                        cmd.Parameters.AddWithValue("@firstname", userProfile.FirstName);
                                        cmd.Parameters.AddWithValue("@lastname", userProfile.LastName);
                                        cmd.Parameters.AddWithValue("@email", userProfile.EMail);
                                        cmd.Parameters.AddWithValue("@username", FilteredUsername);
                                        cmd.Parameters.AddWithValue("@password", userProfile.Password);
                                        cmd.Parameters.AddWithValue("@otp", otp);
                                        cmd.Parameters.Add("@otptime", SqlDbType.DateTime).Value = DateTime.Now;
                                        cmd.Parameters.Add("@datejoined", SqlDbType.DateTime).Value = DateTime.Now;

                                        await cmd.ExecuteNonQueryAsync();
                                        message = "verification email send please verify your account";
                                        type = "success";
                                        Log.Information(userProfile.FirstName + " registered, Email: " + userProfile.EMail);
                                    }
                                    catch (Exception exm)
                                    {
                                        message = "something went wrong";
                                        Log.Error("Error while user registration: " + exm.Message.ToString());
                                    }

                                }
                                else
                                {
                                    message = "unable to send the mail";
                                    type = "error";
                                }
                            }
                            catch (Exception ex2)
                            {
                                message = "something went wrong";
                                Log.Error(ex2.Message.ToString());
                            }
                        }
                        else
                        {
                            message = "username/email taken!!";
                            type = "error";
                        }
                        await connection.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        message = "something went wrong";
                        Log.Error("error while signup" + ex.Message.ToString());
                    }
                }
            }
            else
            {
                message = "something went wrong:";
                type = "error";
            }
            var keys = new { message, type };
            return new JsonResult(keys);
        }

        [HttpGet]
        [Route("/api/getavatars")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GetAvatars()
        {
            try
            {
                List<Avatar> entries = new();
                string sql;
               
                using (SqlConnection connection = new(connectionString))
                {
                    await connection.OpenAsync();
                    sql = "select * from TblAvatarMaster";
                    using SqlCommand command = new(sql, connection);
                    using SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (await dataReader.ReadAsync())
                    {
                        Avatar entry = new()
                        {
                            Id = Int32.Parse(dataReader["Id"].ToString()),
                            Title = (string)dataReader["Title"],
                            Image = (string)dataReader["Image"]
                        };
                        entries.Add(entry);
                    }
                    await connection.CloseAsync();
                }
                Thread.Sleep(2000);
                return new JsonResult(entries);
            }
            catch (Exception ex)
            {
                Log.Error("error in live search: " + ex.Message.ToString());
                return BadRequest("something went wrong");
            }
        }


        [HttpPost]
        [Route("api/profile/update")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SaveProfile([FromBody] UserProfile userProfile)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    using SqlConnection connection = new(connectionString);

                    await connection.OpenAsync();
                    SqlCommand insertCommand = new("UPDATE TblUserProfile SET FirstName = @FirstName,LastName = @LastName,EMail = @EMail,Phone = @Phone,AvatarId= AvatarId,Gender = @Gender,Bio = @Bio where UserName = @UserName", connection);
                    insertCommand.Parameters.AddWithValue("@FirstName", userProfile.FirstName.Trim());
                    insertCommand.Parameters.AddWithValue("@LastName", userProfile.LastName.Trim());
                    insertCommand.Parameters.AddWithValue("@EMail", userProfile.EMail.Trim());
                    insertCommand.Parameters.AddWithValue("@Phone", userProfile.Phone.Trim());
                    insertCommand.Parameters.AddWithValue("@Gender", userProfile.Gender.Trim());
                    insertCommand.Parameters.AddWithValue("@AvatarId", userProfile.AvatarId);
                    insertCommand.Parameters.Add("@datejoined", SqlDbType.DateTime).Value = DateTime.Now;
                    insertCommand.Parameters.AddWithValue("@UserName", HttpContext.Session.GetString("username"));
                    insertCommand.Parameters.AddWithValue("@Bio", userProfile.Bio.Trim());
                    await insertCommand.ExecuteNonQueryAsync();

                    SqlCommand AvatarCommand = new("SELECT Image FROM TblAvatarMaster WHERE Id = @avtrid", connection);
                    AvatarCommand.Parameters.AddWithValue("@avtrid", userProfile.AvatarId);
                    var reader = await AvatarCommand.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        string session_avtr = reader.GetString(0).ToString();
                        HttpContext.Session.SetString("avatar", session_avtr);
                    }
                    HttpContext.Session.SetString("first_name", userProfile.FirstName.Trim());
                    HttpContext.Session.SetString("fullname", userProfile.FirstName.Trim() + " " + userProfile.LastName.Trim());
                    await connection.CloseAsync();
                    return Ok("Changes Saved");
                }
                catch (Exception ex)
                {
                    Log.Error("error updating profile:" + ex.Message.ToString());
                    return BadRequest("Something went wrong");
                }
            }
            else
            {
                return BadRequest("Invalid Data");
            }
          
          
        }


    }
}
