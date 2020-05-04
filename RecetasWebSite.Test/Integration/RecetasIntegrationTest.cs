using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RecetasWebSite.Test.Integration
{
    [TestFixture]
    public class RecetasIntegrationTest
    {
        private RecetasWebApplicationFactory factory;
        private HttpClient client;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            factory = new RecetasWebApplicationFactory();
            client = factory.CreateClient();
        }

        [Test]
        public async Task WhenRouteINotExist_ThenResultIsNotFound()
        {
            var result = await client.GetAsync("/NonExistingRoute");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task GetAllRecetas_ThenResultIsOk()
        {
            var result = await client.GetAsync("/Home");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetReceta_WhenCategoriaAndIdIsPassed_ThenResultIsOk()
        {
            var result = await client.GetAsync("/Home/Details/tartaZanahoria?categoria=Repostería");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetReceta_WhenCategoriaAndIdIsNotPassed_ThenResultIsBadRequest()
        {
            var result = await client.GetAsync("/Home/Details");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            client.Dispose();
            factory.Dispose();
        }
    }
}