using System;
using ExampleCommon;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAPI.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ExampleController : ControllerBase
    {
        [HttpGet]
        [Route("/Version")]
        public string GetVersion() => "1.0";

        [Route("/")]
        [HttpGet]
        public string ExampleObject()
        {
            return "Ahh OK";
        }

        [Route("/Get/{id}/{setItToThis}")]
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
        [Route("/GetThatIsInDevelopment")]
        [ApiVersion("2.0")]
        public string GetInDevelopment() => "Only at v2.0";
    }
}
