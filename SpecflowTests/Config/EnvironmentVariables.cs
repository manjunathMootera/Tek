using System;
using System.Configuration;

namespace SpecflowTests.Config
{
    class EnvironmentVariables
    {
        public static string WebURI
        {
            get { return ConfigurationManager.AppSettings["webURI"]; }
        }

        public static string RecordsCount
        {
            get { return ConfigurationManager.AppSettings["RecordsCount"]; }
        }

        public static string TotalRecordsFoundMessage
        {
            get { return ConfigurationManager.AppSettings["TotalRecordsFoundMessage"]; }
        }

        public static string NoMatchingStateFoundMessage
        {
            get { return ConfigurationManager.AppSettings["NoMatchingStateFoundMessage"]; }
        }
    }
}