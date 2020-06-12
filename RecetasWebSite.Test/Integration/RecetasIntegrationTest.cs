using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecetasWebSite.Test.Integration
{
    /// <summary>
    /// Clase que contiene los tests de integración
    /// </summary>
    [TestFixture]
    public class RecetasIntegrationTest
    {
        #region Campos Privados

        private RecetasWebApplicationFactory factory;
        private HttpClient client;

        #endregion

        #region Configuración de la clase

        /// <summary>
        /// Se llama antes de ejecutar cualquier test
        /// </summary>
        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            factory = new RecetasWebApplicationFactory();
            client = factory.CreateClient();
        }

        /// <summary>
        /// Se ejecuta al final del proceso
        /// </summary>
        [OneTimeTearDown]
        public void TearDown()
        {
            client.Dispose();
            factory.Dispose();
        }

        #endregion

        #region Métodos de Tests

        /// <summary>
        /// Cuando la ruta no existe
        /// </summary>
        /// <returns>DEvuelve un 404 Not Found</returns>
        [Test]
        public async Task WhenRouteNotExist_ThenResultIsNotFound()
        {
            var result = await client.GetAsync("/NonExistingRoute");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        /// <summary>
        /// Obtiene todas las recetas
        /// </summary>
        /// <returns>Devuelve un 200 Ok</returns>
        [Test]
        public async Task GetAllRecetas_ThenResultIsOk()
        {
            var result = await client.GetAsync("/Home");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /// <summary>
        /// Obtiene una receta a partir de una categoria y un Id
        /// </summary>
        /// <returns>Devuelve un 200 Ok</returns>
        [Test]
        public async Task GetReceta_WhenCategoriaAndIdIsPassed_ThenResultIsOk()
        {
            var result = await client.GetAsync("/Home/Details/tartaZanahoria?categoria=Repostería");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /// <summary>
        /// Falla al obtener una receta cuando no se busca por categoria e Id
        /// </summary>
        /// <returns>Devuelve un 500 Internal server error</returns>
        [Test]
        public async Task GetReceta_WhenCategoriaAndIdIsNotPassed_ThenResultIsBadRequest()
        {
            var result = await client.GetAsync("/Home/Details");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        #endregion
    }
}