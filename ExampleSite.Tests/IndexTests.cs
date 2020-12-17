using ExampleSite.HttpClients;
using ExampleSite.Pages;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace ExampleSite.Tests
{
    [TestFixture]
    public class IndexTests
    {
        public IndexModel IndexModel { get; set; }
        public ILogger<IndexModel> FakeLogger { get; set; }
        public IExampleApiClient FakeExampleApiClient { get; set; }

        [SetUp]
        public void SetUp()
        {
            FakeLogger = new NullLogger<IndexModel>();
            FakeExampleApiClient = A.Fake<IExampleApiClient>();
            IndexModel = new IndexModel(FakeLogger, FakeExampleApiClient);
        }

        [Test]
        public void TestSomething()
        {
            Assert.AreEqual("This", IndexModel.SillyMethodToTest());
        }
    }
}
