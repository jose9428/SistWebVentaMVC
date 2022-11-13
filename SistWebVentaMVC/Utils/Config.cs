namespace SistWebVentaMVC.Utils
{
    public static class Config
    {
        private static IConfiguration configuration;

        static Config()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //  .AddJsonFile("archivodos.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();
        }

        public static string Get(string name)
        {
            string appSettings = configuration[name];
            return appSettings;
        }

        public static IConfigurationSection GetSection(string name)
        {
            return configuration.GetSection(name);
        }
    }
}
