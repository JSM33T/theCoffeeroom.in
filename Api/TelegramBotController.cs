using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using theCoffeeroom.Core.Helpers;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Api
{

    [ApiController]
    public class TelegramBotController : ControllerBase
    {

        [HttpPost]
        [Route("api/telegram/post/imagetextbtn")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> PingBot2(TgBot tgBot)
        {   
            var botClient = new TelegramBotClient(tgBot.Token);
            var markdownText = tgBot.Message;
            long chatId = tgBot.UserId;

            try
            {
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                new[]
                {
                    InlineKeyboardButton.WithUrl(tgBot.Btn1Text, tgBot.Btn1Url)
                }
            });

                await botClient.SendPhotoAsync(
                           chatId,
                           photo: InputFile.FromFileId(tgBot.ImgAddress),
                           caption: markdownText,
                           parseMode: ParseMode.Markdown,
                           replyMarkup: inlineKeyboard
                   );

                return Ok("Message sent");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }


    }
}
