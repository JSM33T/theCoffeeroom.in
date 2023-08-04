using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Interfaces;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Controllers.Dedicated
{
    public class MailingListController : Controller
    {
        public readonly IDataAccessRepo _dataAccessRepo;
        public string connectionString = ConfigHelper.NewConnectionString;

        public MailingListController(IDataAccessRepo dataAccessRepo)
        {
            _dataAccessRepo = dataAccessRepo;
        }

        [HttpPost]
        [Route("api/newsletter")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddMailToLetter([FromBody] Mail mail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                            await _dataAccessRepo.AddMailAsync(mail);
                            Log.Information("mail added to newsletter:" + mail.EMailId);
                            return Ok("email submitted!!");
                    }
                    catch (Exception ex)
                    {
                        Log.Error("email submission in page:" + ex.Message.ToString());
                        return BadRequest(ex.Message.ToString());
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
