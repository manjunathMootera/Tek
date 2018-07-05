using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using SpecflowTests.Config;
using SpecflowTests.Services;
using SpecflowTests.Utils;
using System;
using TechTalk.SpecFlow;

namespace SpecflowTests.Specflow.Steps
{
    [Binding]
    class PositiveTestsSteps
    {
        public string[] city = new string[2];
        private IRestResponse _response;
        private dynamic _content;
        private string _state;
        private string _validStateAbbrButInvalidCase = "Oh";
        private string _invalidStateAbbr = "CB";
        private string _invalidStateName = "Columbus";
        private string _validStateNameWithHyphen = "New-Hampshire";
        private APIServices _apiServices = new APIServices();

        [Given(@"I have entered a valid (.*) name")]
        [Given(@"I have entered a valid (.*) of a State")]
        public void GivenIHaveEnteredAValidName(string state)
        {
            _state = state;
        }

        [Given(@"I have access to the US states API")]
        public void GivenIHaveAccessToTheUSStatesAPI()
        {
            // Nothing here
        }

        [Given(@"I have entered an invalid state abbrevation")]
        public void GivenIHaveEnteredAnInvalidStateAbbrevation()
        {
            _state = _invalidStateAbbr;
        }

        [Given(@"I have entered a valid state abbrevation in an invalid case")]
        public void GivenIHaveEnteredAValidStateAbbrevationInAnInvalidCase()
        {
            _state = _validStateAbbrButInvalidCase;
        }

        [Given(@"I have an invalid state name")]
        public void GivenIHaveAnInvalidStateName()
        {
            _state = _invalidStateName;
        }

        [Given(@"I have a state name with a hyphen")]
        public void GivenIHaveAStateNameWithAHyphen()
        {
            _state = _validStateNameWithHyphen;
        }

        [Given(@"I have a request without a state")]
        public void GivenIHaveARequestWithoutAState()
        {
            // nothing here as this will be taken care in the Get request
        }

        [When(@"I execute a Get request")]
        public void WhenIExecuteAGetRequest()
        {
            _response = _apiServices.GetResponse(EnvironmentVariables.WebURI + "/all");
            _content = JObject.Parse(_response.Content);
        }

        [When(@"I execute a Get request for an invalid state abbrevation")]
        public void WhenIExecuteAGetRequestForAnInvalidStateAbbrevation()
        {            
            _response = _apiServices.GetResponse(EnvironmentVariables.WebURI + "/" +_invalidStateAbbr);
            _content = JObject.Parse(_response.Content);
        }

        [When(@"I execute a Get request for an valid state abbrevation with invalid case")]
        public void WhenIExecuteAGetRequestForAnValidStateAbbrevationWithInvalidCase()
        {
            _response = _apiServices.GetResponse(EnvironmentVariables.WebURI + "/" + _invalidStateAbbr);
            _content = JObject.Parse(_response.Content);
        }

        [When(@"I execute a Get request without the state")]
        public void WhenIExecuteAGetRequestWithoutTheState()
        {
            _response = _apiServices.GetResponse(EnvironmentVariables.WebURI + "/");
        }

        [Then(@"I get a (.*) response")]
        public void ThenIGetAResponse(string OK)
        {
            Assert.IsTrue(_response.StatusCode.ToString() == "OK", "The API did not return a OK");
        }

        [Then(@"I get a count of (.*) records")]
        public void ThenIGetACountOfRecords(string recordsCount)
        {
            VerifyAsserts.AreEqual(Convert.ToString(_content.RestResponse.result.Count), EnvironmentVariables.RecordsCount, "The total records of the States/Territories returned by the API is not correct");
            VerifyAsserts.Contains(Convert.ToString(_content.RestResponse.messages), EnvironmentVariables.TotalRecordsFoundMessage, "The total records of found message returned by the API is not right");
        }

        [Then(@"the response should contain the correct (.*) and (.*)")]
        public void ThenTheResponseShouldContainTheCorrectCapitalAndLargestCity(string capital, string largestCity)
        {           
            city = GetCity();

            VerifyAsserts.AreEqual(capital, city[0], "The Capital City of --- " + _state + " --- is not as expected");

            if (largestCity == "")
                largestCity = null;

            VerifyAsserts.AreEqual(largestCity, city[1], "The Largest City of --- " + _state + " --- is not as expected");
        }

        [Then(@"the response should send a invalid state message")]
        public void ThenTheResponseShouldSendAInvalidStateMessage()
        {
            VerifyAsserts.IsNull(_content.RestResponse.count, "The Results is not null. It should have been null for this invalid State Abbrevation");
            VerifyAsserts.Contains(Convert.ToString(_content.RestResponse.messages), EnvironmentVariables.NoMatchingStateFoundMessage, "The no matching state found message is not returned in the response");
        }

        [Then(@"the response will not contain the invalid state")]
        [Then(@"the response will not contain the valid state with a hyphen")]
        public void ThenTheResponseWillNotContainTheInvalidState()
        {
            city = GetCity();

            VerifyAsserts.IsNull(city[0], "The invalid state is found in the response");
        }

        [Then(@"the response will contain a (.*) in it")]
        public void ThenTheResponseWillContainAInIt(int p0)
        {
            Assert.IsTrue(_response.Content.Contains("404"), "The response doesn't have a page not found (404) error in it");
        }

        public string[] GetCity()
        {
            foreach (var item in _content.RestResponse.result)
            {
                if (item.name == _state || item.abbr == _state)
                {
                    city[0] = item.capital;
                    city[1] = item.largest_city;
                    break;
                }
            }
            return city;
        }

        [AfterScenario]
        public static void TearDown()
        {
            if (VerifyAsserts.failureReasons != null)
            {
                var reasons = VerifyAsserts.failureReasons;
                VerifyAsserts.failureReasons = null;
                Assert.Fail(reasons);
            }
        }
    }
}