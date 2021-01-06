using System;
using ExampleCommon;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAPI.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("/v{version:apiVersion}")]
    public class ExampleController : ControllerBase
    {
        [HttpGet]
        [Route("/v{version:apiVersion}/Version")]
        public string GetVersion() => "1.0";

        [HttpGet]
        public string ExampleObject()
        {
            return "Ahh OK";
        }

        [Route("/v{version:apiVersion}/Get/{id}/{setItToThis}")]
        [HttpGet]
        public ExampleObject Get(int id, string setItToThis)
        {
            return new ExampleObject
            {
                Id = id,
                ExampleString = setItToThis,
                WhenItHappened = DateTime.Now
            };
        }

        [HttpGet]
        [ApiVersion("2")]
        public string GetAtV2()
        {
            return "Ahh OK V2";
        }

        [HttpGet]
        [Route("/GetThatIsInDevelopment")]
        [ApiVersion("2")]
        public string GetInDevelopment() => "Only at v2.0";
    }
}
