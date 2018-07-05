using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowTests.Utils
{
    class VerifyAsserts
    {
        public static string failureReasons;

        /// <summary>
        /// This compares the 2 strings
        /// Also continues with the execution of tests even if it fails and then displays the failures at the end of all the tests
        /// </summary>
        /// <param name="actualString">Actual string to compare</param>
        /// <param name="expectedString">Expected string to compare with</param>
        /// <param name="failureMessage"><Failure Message to be displayed></Failure></param>
        public static void AreEqual(string actualString, string expectedString, string failureMessage)
        {
            if (actualString != expectedString)
                failureReasons = failureReasons + "\n" + failureMessage + "\n" + "EXPECTED RESPONSE : --- " + expectedString + " --- ACTUAL RESPONSE : --- " + actualString;    
        }

        /// <summary>
        /// This determines if the one of the string is in the other string
        /// Also continues with the execution of tests even if it fails and then displays the failures at the end of all the tests
        /// </summary>
        /// <param name="fullString">Parent string</param>
        /// <param name="subString">Sub string</param>
        /// <param name="failureMessage"><Failure Message to be displayed></Failure></param>
        public static void Contains(string fullString, string subString, string failureMessage)
        {
            if (!fullString.Contains(subString))
                failureReasons = failureReasons + "\n" + failureMessage + "\n" + "EXPECTED RESPONSE : --- " + subString + " --- ACTUAL RESPONSE : --- " + fullString;
        }

        /// <summary>
        /// This is for the null check on the passed in string
        /// </summary>
        /// <param name="actualResult"><Actual string to verify for null></Actual></param>
        /// <param name="failureMessage"><Failure Message to be displayed></Failure></param>
        public static void IsNull(string actualResult, string failureMessage)
        {
            if (actualResult != null)
                failureReasons = failureReasons + "\n" + failureMessage;
        }
    }
}
