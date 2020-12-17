using ExampleAPI.Controllers;
using NUnit.Framework;

namespace ExampleApi.Tests
{
    [TestFixture]
    public class ExampleControllerTests
    {
        public ExampleController ExampleController { get; set; }

        [SetUp]
        public void SetUp()
        {
            ExampleController = new ExampleController();
        }

        [Test]
        public void TestApiCall()
        {
            Assert.AreEqual("Ahh OK", ExampleController.ExampleObject());
        }
    }
}
