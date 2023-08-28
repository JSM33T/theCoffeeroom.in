using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Data;
using theCoffeeroom.Core;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;
using theCoffeeroom.Models.Frame;
using Validators = theCoffeeroom.Core.Helpers.Validators;

namespace theCoffeeroom.Controllers.Dedicated
{
    //[AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {

        readonly string connectionString = ConfigHelper.NewConnectionString;

        [HttpPost("/api/account/login")]
        [ValidateAntiForgeryToken]
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
                    "where (p.UserName = @username OR p.email = @username)" +
                    " and CryptedPassword =@password" +
                    " and p.IsActive= 1 " +
                    " and p.IsVerified = 1 " +
                    " and p.AvatarId = a.Id", connection);
                checkcommand.Parameters.AddWithValue("@username", loginCreds.UserName.ToLower());
                checkcommand.Parameters.AddWithValue("@password",Core.EnDcryptor.Encrypt(loginCreds.Password));
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
                        Log.Information(loginCreds.UserName + " logged in");
                        return Ok("logging in...");
                       
                    }
                    else
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserSignUp([FromBody] UserProfile userProfile)
        {
            string body, subject;
           
            string message = "something working", type = "error";
            if (userProfile.UserName != null && userProfile.Password!= null)
            {
                if (userProfile.FirstName.Trim() == "")
                {
                    message = "first name is mandatory";
                }
                else if (userProfile.UserName.Trim() == "")
                {
                    message = "username is mandatory";
                }
                else if (!Validators.IsAlphaNumeric(userProfile.UserName.Trim()))
                {
                    message = "only alphanumeric characters are allowed";
                }
                else if (userProfile.EMail.Trim() == "")
                {
                    message = "email is mandatory";
                }
                else if (Validators.IsValidEmail(userProfile.EMail.Trim()) == false)
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

                                body = "<!DOCTYPE html><html><head><meta name=\"viewport\" content=\"width=device-width,initial-scale=1\"><title>Verification Confirmation</title></head><body style=\"margin:0;padding:0;font-family:Arial,sans-serif;line-height:1.4;color:#111;background-color:#fff\"><div style=\"max-width:600px;margin:0 auto;background-color:#fff;padding:20px;border-radius:5px\"><h1 style=\"color:#111;margin-bottom:20px;font-size:24px\">Complete Signup</h1><p>Hey there,</p><div style=\"text-align:center;margin-bottom:20px\"><img src=\"https://thecoffeeroom.in/assets/favicon/apple-touch-icon.png\" width=\"100\" alt=\"Image\" style=\"max-width:100%;height:auto;border-radius:5px\"></div><p>Thank you for signing up for the coffeeroom. Please click the button below to verify your email address:</p><p>" +
                                    "<a href=\"https://thecoffeeroom.in/account/verification/" + FilteredUsername + "/" + otp + "\"" +
                                    " style=\"display:inline-block;padding:10px 20px;background-color:#111;color:#fff;text-decoration:none;border-radius:4px\">Verify Email</a></p><p>If you did not sign up for this account, please ignore this email.</p><div style=\"margin-top:20px;text-align:center;font-size:12px;color:#999\"><p>This is an automated email, please do not reply.</p></div></div></body></html>";

                                //body = "<h1>Hey there,</h1>" +
                                //        "<p> This is for the verification of your account @TheCoffeeRoom." +
                                //        "" + otp + " is your OTP which is valid for 30 minutes </p>." +
                                //        "Or alternatively you can click here to verify directly:" +
                                //        "<button type=\"button\" href=\"https://thecoffeeroom.in/account/verification/" + FilteredUsername + "/" + otp + "\"><b> VERIFY </b></button>";

                                int stat = Mailer.MailSignup(subject, body, userProfile.EMail.ToString());
                                if (stat == 1)
                                {
                                    try
                                    {
                                        SqlCommand maxIdCommand = new("SELECT ISNULL(MAX(Id), 0) + 1 FROM TblUserProfile", connection);
                                        int newId = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                                        cmd = new("insert into TblUserProfile (Id,FirstName,LastName,EMail,UserName,IsActive,IsVerified,OTP,OTPTime,Role,Bio,Gender,Phone,AvatarId,DateJoined,CryptedPassword) VALUES(@Id,@firstname,@lastname,@email,@username,1,0,@otp,@otptime,'user','','','',1,@datejoined,@cryptedpassword)", connection);
                                        cmd.Parameters.AddWithValue("@Id", newId);
                                        cmd.Parameters.AddWithValue("@firstname", userProfile.FirstName.Trim());
                                        cmd.Parameters.AddWithValue("@lastname", userProfile.LastName);
                                        cmd.Parameters.AddWithValue("@email", userProfile.EMail);
                                        cmd.Parameters.AddWithValue("@username", FilteredUsername.Trim());
                                        cmd.Parameters.AddWithValue("@cryptedpassword",EnDcryptor.Encrypt(userProfile.Password.Trim()));
                                        cmd.Parameters.AddWithValue("@otp", otp.Trim());
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
                //Thread.Sleep(2000);
                return new JsonResult(entries);
            }
            catch (Exception ex)
            {
                Log.Error("error in live search: " + ex.Message.ToString());
                return BadRequest("something went wrong");
            }
        }


        [HttpPost]
        [Route("api/profile/password/update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword([FromBody] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                if (userProfile.Password == HttpContext.Session.GetString("username").ToString())
                {
                    return BadRequest("Password can't be similar to your username");
                }
                else
                {
                    try
                    {
                        string LoggedUser = HttpContext.Session.GetString("username").ToString();
                        using SqlConnection connection = new(connectionString);

                        await connection.OpenAsync();
                        SqlCommand insertCommand = new("UPDATE TblUserProfile SET CryptedPassword = @cryptedpassword,DateUpdated = @dateupdated where UserName = @username", connection);
                        insertCommand.Parameters.AddWithValue("@username", LoggedUser);
                        insertCommand.Parameters.AddWithValue("@cryptedpassword", Core.EnDcryptor.Encrypt(userProfile.Password));
                        insertCommand.Parameters.Add("@dateupdated", SqlDbType.DateTime).Value = DateTime.Now;

                        await insertCommand.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                        Log.Information(LoggedUser + " changed their password");
                        return Ok("Changes Saved");
                    }
                    catch (Exception ex)
                    {
                        Log.Error("error updating profile:" + ex.Message.ToString());
                        return BadRequest("Something went wrong");
                    }
                }
               
            }
            else
            {
                return BadRequest("Invalid Password Format");
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
                    SqlCommand insertCommand = new("UPDATE TblUserProfile SET FirstName = @FirstName,LastName = @LastName,Phone = @Phone,AvatarId= @AvatarId,Gender = @Gender,Bio = @Bio where UserName = @UserName", connection);
                    insertCommand.Parameters.AddWithValue("@FirstName", userProfile.FirstName.Trim());
                    insertCommand.Parameters.AddWithValue("@LastName", userProfile.LastName.Trim());
                    insertCommand.Parameters.AddWithValue("@Phone", userProfile.Phone.Trim());
                    insertCommand.Parameters.AddWithValue("@Gender", userProfile.Gender.Trim());
                    insertCommand.Parameters.AddWithValue("@AvatarId", userProfile.AvatarId);
                    insertCommand.Parameters.Add("@dateupdated", SqlDbType.DateTime).Value = DateTime.Now;
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

        [HttpPost]
        [Route("api/account/recover")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecoverAccount([FromBody] GenericReceiver genericReceiver)
        {
            //UserProfile userProfile = null ;
            if (ModelState.IsValid)
            {
                try
                {
                    using SqlConnection connection = new(connectionString);
                    SqlCommand CheckCommand = new("SELECT * FROM TblUserProfile WHERE UserName = @username or EMail = @username", connection);
                    CheckCommand.Parameters.AddWithValue("@username", genericReceiver.StringRec);
                    await connection.OpenAsync();
                    using SqlDataReader reader = CheckCommand.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        int Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        string Username = reader.GetString(reader.GetOrdinal("UserName"));
                        string Useremail = reader.GetString(reader.GetOrdinal("EMail"));
                        string secret = StringProcessors.GenerateRandomString(10);
                        var otp = OTPGenerator.GenerateOTP(secret);
                       string subject = "Recover Your Account | TheCoffeeroom";
                        await reader.CloseAsync();
                        try
                        {

                         string body = "<h1>Hey there,</h1><br> Here is an OTP to log into your account. It will be valid for a day do change your acc password within a day. You can log into your account using this OTP only once &nbsp;<br> <h1><b>" + otp + "</b></h1>";

                            int stat = Mailer.MailSignup(subject, body, Useremail);
                            if (stat == 1)
                            {
                                try
                                {
                                    SqlCommand maxIdCommand = new("SELECT ISNULL(MAX(Id), 0) + 1 FROM TblPasswordReset", connection);
                                    int newId = Convert.ToInt32(maxIdCommand.ExecuteScalar());
                                    SqlCommand cmd = new("insert into TblPasswordReset (Id,UserId,Token,DateAdded,IsValid) VALUES(@id,@userid,@token,@dateadded,@isvalid)", connection);
                                    cmd.Parameters.AddWithValue("@id", newId);
                                    cmd.Parameters.AddWithValue("@userid", Id);
                                    cmd.Parameters.AddWithValue("@token", otp);
                                    cmd.Parameters.AddWithValue("@isvalid", true);
                                    cmd.Parameters.Add("@dateadded", SqlDbType.DateTime).Value = DateTime.Now;

                                    await cmd.ExecuteNonQueryAsync();
                                    return Ok("OTP sent to your mail please enter the OTP to login");
                                }
                                catch (Exception exm)
                                {
                                    Log.Error("Error while user registration: " + exm.Message.ToString());
                                    return BadRequest("Something went wrong");
                                }
                            }
                            else
                            {
                                //log to be added later
                                return BadRequest("Unable to send mail");
                            }
                        }
                        catch (Exception ex2)
                        {
                            Log.Error(ex2.Message.ToString());
                            return BadRequest("Something went wrong");
                        }
                    }
                    else
                    {
                        await connection.CloseAsync();
                        return BadRequest("no record found with given username/email");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("error updating profile:" + ex.Message.ToString());
                    return BadRequest("Something went wrong");
                }
            }
            else
            {
                return BadRequest("Invalid Username/Email!!");
            }


        }

        [HttpPost]
        [Route("api/account/loginviaotp")]
        [IgnoreAntiforgeryToken]
        public  IActionResult LogInViaOtp([FromBody] LoginCreds loginCreds)
        {
            try
            {

                using SqlConnection connection = new(connectionString);
                connection.Open();

                SqlCommand checkOTP = new("SELECT * FROM TblPasswordReset WHERE Token = @otp", connection);
                checkOTP.Parameters.AddWithValue("@otp", loginCreds.Otp);

                using SqlDataReader readera = checkOTP.ExecuteReader();
                string currUserId = "";

                if (readera.Read())
                {
                    currUserId = readera.GetInt32(readera.GetOrdinal("UserId")).ToString();
                }
                else
                { currUserId = ""; };
                readera.Close();
                    SqlCommand checkcommand = new("select p.*,a.Image " +
                       "from TblUserProfile p,TblAvatarMaster a " +
                       "where p.Id = @id " +
                       "and p.IsActive= 1 " +
                       "and p.IsVerified = 1 " +
                       "and p.AvatarId = a.Id", connection);
                    checkcommand.Parameters.AddWithValue("@id", currUserId);
                    using var reader = checkcommand.ExecuteReader();
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
                        connection.Close();
                        return Ok("logging in...");

                    }
                    else
                    {
                        connection.Close();
                        return BadRequest("Invalid/Expired Otp");
                    }
            }
            catch(Exception ex2)
            {
                Log.Error(ex2.Message.ToString());
                return BadRequest("Something went wrong");
            }
            

           
        }

        [HttpGet]
        [Route("/api/profile/getdetails")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Index()
        {
            UserProfile userProfile = null;
            string connectionString = ConfigHelper.NewConnectionString;
            var sessionStat = HttpContext.Session.GetString("role");

            if (sessionStat != null && (sessionStat == "user" || sessionStat == "admin"))
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT a.*,b.Image FROM TblUserProfile a, TblAvatarMaster b WHERE UserName = @Id and a.AvatarId = b.Id", connection);
                command.Parameters.AddWithValue("@Id", HttpContext.Session.GetString("username"));
                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    userProfile = new UserProfile()
                    {
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        UserName = reader.GetString(3),
                        Role = reader.GetString(10),
                        EMail = reader.GetString(4),
                        Phone = reader.GetString(5),
                        Gender = reader.GetString(6),
                        Bio = reader.GetString(11),
                        AvatarImg = reader.GetString(17)
                    };
                }

                await reader.CloseAsync();
                await connection.CloseAsync();

                return Json(userProfile); 
            }
            else
            {
                return Json(new { message = "Access denied" }); 
            }
        }

    }
}
