using Serilog;
using System.Net.Mail;
using System.Net;

namespace theCoffeeroom.Core.Helpers
{
    public class Mailer
    {
        public static int MailSignup(string subject, string body, string email)
        {
            int msg;
            // string encrotp = Cryptolib.Encrypt(otp, ConfigHelper.CryptoKey.ToString()).ToString();
            // string tobase64 = encrotp.Replace('/', '_');
            try
            {
                //MailMessage message = new();
                //message.From = new MailAddress("mail@jsm33t.com","Jsm33t.com");
                //message.To.Add(email);
                //message.Subject = subject;
                //message.Body = body;
                //message.IsBodyHtml = true;
                //SmtpClient smtpClient = new();
                //smtpClient.Host = "jsm33t.com";
                //smtpClient.Port = 587; // or 465 for SSL
                //smtpClient.EnableSsl = false; // or true for SSL
                //smtpClient.Credentials = new NetworkCredential("mail@jsm33t.com", ConfigHelper.WebMailKey.ToString());
                //smtpClient.Timeout = 10000;
                //smtpClient.Send(message);
                MailMessage message = new MailMessage
                {
                    From = new MailAddress("mail@thecoffeeroom.in", "thecoffeeroom.in"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                message.To.Add(email);
                SmtpClient smtpClient = new SmtpClient
                {
                    Host = "thecoffeeroom.in",
                    Port = 587,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("mail@thecoffeeroom.in", ConfigHelper.WebMailKey.ToString()),
                    Timeout = 10000
                };

                smtpClient.Send(message);
                Log.Information("signup mail send - successfull");
                msg = 1;
            }
            catch (Exception ex)
            {
                msg = 0;
                Log.Error("error while signup-mailing: " + ex.Message.ToString());
            }

            return msg;
        }
        public static int MailRecoverPassword(string otp, string uzrnm, string email)
        {
            int msg;
            // string encrotp = Cryptolib.Encrypt(otp, ConfigHelper.CryptoKey.ToString()).ToString();
            // string tobase64 = encrotp.Replace('/', '_');
            try
            {
                MailMessage message = new()
                {
                    From = new MailAddress("mail@thecoffeeroom.in"),
                    Subject = "SignUp verification for thecoffeeroom.in",
                    Body = "<h1>Hey there,</h1><p>This is for the verification of your account. " + otp + " is your OTP which is valid for 30 minutes</p>.Or alternatively you can click here to verify directly: <a href=\"https://thecoffeeroom.in/account/verify?uzrnm=" + uzrnm + "&key=" + otp + "\"><B>VERIFY</B></a>",
                    IsBodyHtml = true
                };
                message.To.Add(email);
                SmtpClient smtpClient = new()
                {
                    Host = "thecoffeeroom.in",
                    Port = 587, // or 465 for SSL
                    EnableSsl = false, // or true for SSL
                    Credentials = new NetworkCredential("mail@thecoffeeroom.in", ConfigHelper.WebMailKey.ToString()),
                    Timeout = 10000
                };
                smtpClient.Send(message);
                msg = 1;
            }
            catch (Exception ex)
            {
                msg = 0;
                Log.Information(ex.Message);
            }

            return msg;
        }
    }
}
