using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ExampleCommon;
using Newtonsoft.Json;

namespace ExampleSite.HttpClients
{
    public class ExampleApiClient : IExampleApiClient
    {
        public ExampleApiClient()
        {
            
        }

        public async Task<(ExampleObject, HttpStatusCode)> GetObject(string baseUrl, int Id, string someValue)
        {
            var fullUri = baseUrl + "/Get/" + Id + "/" + someValue + "/";

            using var client = new HttpClient();

            try
            {
                using var response = await client.GetAsync(fullUri);
                var data = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return (null,
                        response.StatusCode);
                var exampleObject = JsonConvert.DeserializeObject<ExampleObject>(data);

                return (exampleObject,
                    response.StatusCode);

            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem connecting to Provider API.", ex);
            }
        }

        public async Task<string> GetString(string baseUrl)
        {
            var fullUri = baseUrl;

            using var client = new HttpClient();

            try
            {
                using var response = await client.GetAsync(fullUri);
                var data = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return (null);

                return (data);

            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem connecting to Provider API.", ex);
            }
        }
    }
}
