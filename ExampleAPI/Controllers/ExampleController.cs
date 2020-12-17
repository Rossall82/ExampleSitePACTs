using System;
using ExampleCommon;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
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
    }
}
