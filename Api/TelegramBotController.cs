using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using theCoffeeroom.Services.Helpers;
using theCoffeeroom.Models.Domain;

namespace theCoffeeroom.Api
{

    [ApiController]
    public class TelegramBotController : ControllerBase
    {

        [HttpPost]
        [Route("api/telegram/post/imagetextbtn")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> MakePost(TgBot tgBot)
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

        [HttpPost]
        [Route("api/telegram/post/imagestextbtn")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> MakePostWithImages(TgBot tgBot)
        {
            var botClient = new TelegramBotClient(tgBot.Token);
            long chatId = tgBot.UserId;

            try
            {
                // Create the inline keyboard
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl(tgBot.Btn1Text, tgBot.Btn1Url)
                    }
                });

                
                var media = new List<InputMediaPhoto>
                    {
                        new InputMediaPhoto(tgBot.ImgAddress1),  // Assuming tgBot.ImgAddress1 is a valid file ID or URL for an image
                        new InputMediaPhoto(tgBot.ImgAddress2)   // Assuming tgBot.ImgAddress2 is a valid file ID or URL for another image
                    };


                // Send the media group with captions and the inline keyboard
                await botClient.SendMediaGroupAsync(
                    chatId,
                    media,
                    disableNotification: false
                );

                // Send the text message separately
                await botClient.SendTextMessageAsync(
                    chatId,
                    text: tgBot.Message,
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
