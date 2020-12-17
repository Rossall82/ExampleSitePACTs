using System.Net;
using System.Threading.Tasks;
using ExampleCommon;

namespace ExampleSite.HttpClients
{
    public interface IExampleApiClient
    {
        Task<(ExampleObject, HttpStatusCode)> GetObject(string baseUrl, int Id, string someValue);
        Task<string> GetString(string baseUrl);
    }
}