using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Interfaces;
using theCoffeeroom.Models.DAModels;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Api
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMailToLetter([FromBody] Mail mail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        DataSave result = await _dataAccessRepo.AddMailAsync(mail);

                        if (result.Status)
                        {
                            return Ok(result.Message);
                        }
                        else
                        {
                            return BadRequest(result.Message);
                        }

                    }
                    catch (Exception ex)
                    {
                        Log.Error("email submission in page:" + ex.Message.ToString());
                        return BadRequest("something went wrong");
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
