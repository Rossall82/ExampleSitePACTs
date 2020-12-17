using System;
using System.Net;
using System.Threading.Tasks;
using ExampleSite.HttpClients;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ExampleSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IExampleApiClient _exampleApiClient;

        public int Id { get; set; }
        public string TheValue { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IExampleApiClient exampleApiClient)
        {
            _logger = logger;
            _exampleApiClient = exampleApiClient;
        }

        public void OnGet()
        {
            var ludicrous = SillyMethodToTest();
        }

        /// <summary>
        /// We're going to post to the API
        /// </summary>
        public async Task OnPostAsync()
        {
            var itGaveUs = await _exampleApiClient.GetObject(
                "https://exampleapi20201030092337.azurewebsites.net/", 1, "Wowzers");

            if (itGaveUs.Item2 != HttpStatusCode.OK)
            {
                throw new ApplicationException("Unable to retrive object!");
            }

            Id = itGaveUs.Item1.Id;
            TheValue = itGaveUs.Item1.ExampleString;
        }

        public string SillyMethodToTest()
        {
            return "This";
        }
    }
}
