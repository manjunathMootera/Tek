using CapitalAndLargestCityOfStates.Config;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace CapitalAndLargestCityOfStates
{
    public class ConsoleApp
    {
        public string[] city = new string[2];
        private string _apiServiceURI;
        private IRestResponse _response;
        private dynamic _content;
        private static string[] _capitalAndLargestCity; // = new string[2];

        public static void Main(string[] args)
        {
            ConsoleApp cs = new ConsoleApp();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("This app tells you the largest city and capital of any State in the US \n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Enter EXIT at anytime from your keyboard to exit the app");
            Console.ResetColor();

            while (true)
            {
                
                Console.WriteLine("\nEnter the State Name or the State Abbrevation. To exit type EXIT");
                string state = Console.ReadLine();

                if (state.ToLower() == "exit")
                {
                    break;
                }

                if (state.Length == 2)
                {
                    state = state.ToUpper();
                    _capitalAndLargestCity = cs.GetCapitalAndLargestCityByAbbrevation(state);

                }
                else
                {
                    state = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(state.ToLower());
                    _capitalAndLargestCity = cs.GetCapitalAndLargestCityByName(state);
                }

                if (_capitalAndLargestCity[0] == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThe entered state or abbrevation is not valid. Please enter a valid one. To exit type EXIT");
                    Console.ResetColor();
                }
                else if (_capitalAndLargestCity[1] == null)
                {
                    Console.WriteLine("\n The Capital City of " + state + " is : " + _capitalAndLargestCity[0]);
                    Console.WriteLine("\n The Largest City of " + state + " is : " + "This state is a US territory and doesn't have a city");
                }
                else
                {
                    Console.WriteLine("\n The Capital City of " + state + " is : " + _capitalAndLargestCity[0]);
                    Console.WriteLine("\n The Largest City of " + state + " is : " + _capitalAndLargestCity[1]);
                }
            }
        }

        public string[] GetCapitalAndLargestCityByAbbrevation(string stateAbbervation)
        {
            CleanTheCity();
            _response = GetResponse(EnvironmentVariables.WebURI + "/" + stateAbbervation);
            _content = JObject.Parse(_response.Content);

            try
            {
                city[0] = _content.RestResponse.result.capital;
                city[1] = _content.RestResponse.result.largest_city;
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                city[0] = null;
                city[1] = null;
            }

            return city;
        }

        public string[] GetCapitalAndLargestCityByName(string stateName)
        {
            CleanTheCity();
            _apiServiceURI = EnvironmentVariables.WebURI + "/all";

            _response = GetResponse(_apiServiceURI);
            _content = JObject.Parse(_response.Content);

            foreach (var item in _content.RestResponse.result)
            {
                if (item.name == stateName)
                {
                    city[0] = item.capital;
                    city[1] = item.largest_city;
                    break;
                }
            }

            return city;
        }

        public string[] CleanTheCity()
        {
            city[0] = null;
            city[1] = null;
            return city;
        }

        public IRestResponse GetResponse(string uRI)
        {
            var client = new RestClient(uRI);
            var request = new RestRequest(Method.GET);
            return client.Execute(request);
        }
    }
}