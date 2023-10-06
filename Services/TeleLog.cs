using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using theCoffeeroom.Models.Domain;
using Serilog;
using theCoffeeroom.Services.Helpers;

namespace theCoffeeroom.Services
{
    public class TeleLog
    {
        readonly static string botToken = ConfigHelper.TeleBotToken;
        public static async Task Logstuff(string LogMessage)
        {
            //var botClient = new TelegramBotClient(botToken);

            //var markdownText = LogMessage;

            //long chatId = long.Parse(ConfigHelper.TelelogId);

            //try
            //{
            //    await botClient.SendTextMessageAsync(chatId, markdownText, parseMode: ParseMode.Markdown);
            //}

            //catch (Exception ex)
            //{
            //    Log.Error("exception in telelogging: " + ex.Message.ToString());
            //}
        }

    }
}
