namespace theCoffeeroom.Core
{
    using Serilog;
    using Serilog.Core;
    using Serilog.Events;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class TelegramSink : ILogEventSink
    {
        private readonly string telegramBotToken;
        private readonly long chatId;
        private readonly HttpClient httpClient;

        public TelegramSink(string botToken, long chatId)
        {
            this.telegramBotToken = botToken;
            this.chatId = chatId;
            this.httpClient = new HttpClient();
        }

        public void Emit(LogEvent logEvent)
        {
            try
            {
                var message = logEvent.RenderMessage();
                var apiUrl = $"https://api.telegram.org/bot{telegramBotToken}/sendMessage";
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("chat_id", chatId.ToString()),
                new KeyValuePair<string, string>("text", message),
            });

                var response = httpClient.PostAsync(apiUrl, content).Result;
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Handle errors and exceptions here
                Console.WriteLine($"Error sending log message to Telegram: {ex.Message}");
            }
        }
    }
}
