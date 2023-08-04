namespace theCoffeeroom.Core.Helpers
{
    public class ConfigHelper
    {
            private static readonly IConfigurationRoot _config;

            static ConfigHelper() => _config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

        #pragma warning disable CS8603 // Possible null reference return.
        public static string NewConnectionString => _config.GetConnectionString("coffeeString");
        
        public static string WebMailKey => _config["WebMailPassword"];

        public static string CryptoKey => _config["EncryptionKey"];

        #pragma warning restore CS8603 // Possible null reference return.
    }
}
