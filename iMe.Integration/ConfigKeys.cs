using System.Configuration;

namespace iMe.Integration
{
    public static class ConfigKeys
    {
        #region Twitter settings

        public static string ConsumerKey => ConfigurationManager.AppSettings["ConsumerKey"];

        public static string ConsumerSecret => ConfigurationManager.AppSettings["ConsumerSecret"];

        #endregion Twitter Settings

        #region GitHub Settings

        public static string GitHubUserApiSearchUrl => ConfigurationManager.AppSettings["GitHubUserApiSearchUrl"];
        #endregion
    }
}
