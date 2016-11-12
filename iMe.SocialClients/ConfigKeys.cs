using System.Configuration;

namespace iMe.SocialClients
{
    public static class ConfigKeys
    {
        #region Twitter settings

        public static string ConsumerKey => ConfigurationManager.AppSettings["ConsumerKey"];

        public static string ConsumerSecret => ConfigurationManager.AppSettings["ConsumerSecret"];

        #endregion TwitterSettings
    }
}
