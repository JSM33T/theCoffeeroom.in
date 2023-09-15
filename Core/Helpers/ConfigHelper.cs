namespace theCoffeeroom.Core.Helpers
{
    public class ConfigHelper
    {
        private static readonly IConfigurationRoot _config;

        static ConfigHelper() => _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

        public static string NewConnectionString => _config.GetConnectionString("coffeeString");
        
        public static string WebMailKey => _config["WebMailPassword"];

        public static string CryptoKey => _config["EncryptionKey"];
        public static string TeleBotToken => _config["TeleBotToken"];
        public static string TelelogId => _config["TeleLogChannelId"];
    }
}
