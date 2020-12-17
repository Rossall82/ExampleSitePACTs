using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleSite.HttpClients;
using NUnit.Framework;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;

namespace ExampleSite.Tests
{
    [TestFixture]
    public class ExampleApiClientTests
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;
        private ConsumerExampleApiPact _consumerExampleApiPact;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _consumerExampleApiPact = new ConsumerExampleApiPact();
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _consumerExampleApiPact.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            _mockProviderService = _consumerExampleApiPact.MockProviderService;
            _mockProviderService
                .ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run

            _mockProviderServiceBaseUri = _consumerExampleApiPact.MockProviderServiceBaseUri;
        }

        [Test]
        public async Task TestGetOnRootOfExampleApiReturnsExpectedThing()
        {
            _mockProviderService.Given("I just hit controller root")
                .UponReceiving("A GET request at the root")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/"
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        {"Content-Type", "application/json; charset=utf-8"}
                    },
                    Body = "Ahh OK"
                });

            var consumer = new ExampleApiClient();

            var result = await consumer.GetString(_mockProviderServiceBaseUri);

            Assert.AreEqual("Ahh OK", result);

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }

        [Test]
        public async Task TestGetOnExampleApiWithParamsReturnsExpectedThing()
        {
            _mockProviderService.Given("I provide param arguments")
                .UponReceiving("A GET request to return a populated ExampleObject with params")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/Get/1/test/",
                    Headers = new Dictionary<string, object>
                    {
                        {"Host", "localhost:9222"},
                        {"Version", "HTTP/1.1"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new
                    {
                        Id = 1,
                        WhenItHappened = DateTime.Now,
                        ExampleString = "test"
                    }

                });

            var consumer = new ExampleApiClient();

            var result = await consumer.GetObject(_mockProviderServiceBaseUri, 1, "test");

            Assert.AreEqual(1, result.Item1.Id);
            Assert.AreEqual("test", result.Item1.ExampleString);

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }
    }
}
