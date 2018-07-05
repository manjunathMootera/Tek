using System.Configuration;

namespace CapitalAndLargestCityOfStates.Config
{
    class EnvironmentVariables
    {
        public static string WebURI
        {
            get { return ConfigurationManager.AppSettings["webURI"]; }
        }
    }
}