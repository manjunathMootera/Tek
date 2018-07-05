using RestSharp;

namespace SpecflowTests.Services
{
    class APIServices
    {
        public IRestResponse GetResponse(string uRI)
        {
            var client = new RestClient(uRI);
            var request = new RestRequest(Method.GET);
            return client.Execute(request);
        }
    }
}