namespace BrivoAPIIntegration.Configuration
{
    public class ConfigurationSettings
    {
        private static ConfigurationSettings _instance = null;
        private static readonly object padlock = new object();
        private static IConfiguration configuration;

        #region Object Creation
        private ConfigurationSettings()
        {
            configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();
        }

        #endregion

        public static ConfigurationSettings Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigurationSettings();
                    }
                    return _instance;
                }
            }
        }

        public string APIURL
        {
            get => configuration.GetValue<string>("BrivoAPIURL");
        }

        public string APITokenURL
        {
            get => configuration.GetValue<string>("BrivoAPITokenURL");
        }

        public string AuthorizationKey
        {
            get => configuration.GetValue<string>("AuthorizationKey");
        }

        public string APIKey
        {
            get => configuration.GetValue<string>("APIKey");
        }
    }
}
