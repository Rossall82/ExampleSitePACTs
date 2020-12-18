using System.Collections.Generic;
using ExampleAPI.Controllers;
using NUnit.Framework;
using PactNet;
using PactNet.Infrastructure.Outputters;

namespace ExampleApi.Tests
{
    [TestFixture]
    public class ExampleControllerTests
    {
        private string _providerUri { get; }

        public ExampleController ExampleController { get; set; }

        public ExampleControllerTests()
        {
            _providerUri = "http://localhost:1342";
        }

        [SetUp]
        public void SetUp()
        {
            ExampleController = new ExampleController();
        }

        [Test]
        public void EnsureProviderApiHonoursPactWithConsumer()
        {
            // Arrange
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new ConsoleOutput()
                },
                Verbose = true
            };

            // Act / Assert
            IPactVerifier pactVerifier = new PactVerifier(config);
                pactVerifier.ServiceProvider("ExampleApi", _providerUri)
                .HonoursPactWith("ExampleSite")
                .PactUri(@"pathtopactfile\examplesite-exampleapi.json")
                .Verify();
        }

        [Test]
        public void TestApiCall()
        {
            Assert.AreEqual("Ahh OK", ExampleController.ExampleObject());
        }
    }
}
