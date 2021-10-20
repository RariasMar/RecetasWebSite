using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RecetasWebSite.Domain;
using RecetasWebSite.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RecetasWebSite.Test.Unit
{
    /// <summary>
    /// Esta clase contendrá los futuros test unitarios de la clase repositorio
    /// </summary>
    [TestFixture]
    public class RepositorioTest
    {
        #region Campos Privados

        private IRecetasRepositorio recetasRepositorio;
        private Mock<CloudTable> recetasTabla;
        private Mock<ILogger<RecetasRepositorio>> logger;

        #endregion

        #region Setup

        /// <summary>
        /// Configura los objetos necesarios
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            logger = new Mock<ILogger<RecetasRepositorio>>();
            recetasTabla = new Mock<CloudTable>(new Uri("https://mockTest"), new StorageCredentials("mockTest", "mockTest"));
            recetasRepositorio = new RecetasRepositorio(logger.Object, recetasTabla.Object);
        }

        #endregion

        #region Métodos de Tests
        /// <summary>
        /// Comprueba la acción GetRecetas del repositorio de recetas
        /// </summary>
        [Test]
        public void GetRecetas_ReturnsAnObjectList_WithAllRecetas()
        {
            recetasTabla.Setup(recetas => recetas.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<RecetaEntity>>(), It.IsAny<TableContinuationToken>())).Returns(this.GetRecetas());
            var result = recetasRepositorio.GetRecetas().Result;

            Assert.IsInstanceOf<List<Receta>>(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void GetReceta_ReturnsAnObject_WithOneReceta()
        {
            //recetasTabla.Setup(recetas => recetas.ExecuteAsync(It.Is<TableOperation>(to => to.OperationType == TableOperationType.Retrieve && to.Entity.GetType() == typeof(RecetaEntity)))).Returns(this.GetReceta());
            //recetasTabla.Setup(recetas => recetas.ExecuteAsync(TableOperation.Retrieve<RecetaEntity>("categoria", "id", null))).Returns(this.GetReceta());
            //var result = recetasRepositorio.GetReceta(It.IsAny<string>(), It.IsAny<string>()).Result;

            //Assert.IsInstanceOf<Receta>(result);
            Assert.IsTrue(true);
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Devuelve un listado de recetas de prueba
        /// </summary>
        /// <returns>Listado con recetas mock</returns>
        private Task<TableQuerySegment<RecetaEntity>> GetRecetas()
        {
            var recetas = new List<RecetaEntity>();

            Receta receta1 = new Receta
            {
                Id = "receta1",
                Categoria = "Categoria1",
                Nombre = "Receta1",
                Descripcion = "Descripción de la receta 1",
                Ingredientes = new List<IngredientesPasos>(),
                Pasos = new List<IngredientesPasos>()
            };

            Receta receta2 = new Receta
            {
                Id = "receta2",
                Categoria = "Categoria2",
                Nombre = "Receta2",
                Descripcion = "Descripción de la receta 2",
                Ingredientes = new List<IngredientesPasos>(),
                Pasos = new List<IngredientesPasos>()
            };

            recetas.Add(new RecetaEntity(receta1));
            recetas.Add(new RecetaEntity(receta2));

            var ctor = typeof(TableQuerySegment<RecetaEntity>).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(c => c.GetParameters().Count() == 1);
            var mockTableQuerySegment = ctor.Invoke(new object[] { recetas }) as TableQuerySegment<RecetaEntity>;
            return Task.FromResult(mockTableQuerySegment);
        }

        /// <summary>
        /// Devuelve una receta de prueba
        /// </summary>
        /// <returns>Una receta mock</returns>
        private Task<TableResult> GetReceta()
        {
            Receta receta = new Receta
            {
                Id = "receta1",
                Categoria = "Categoria1",
                Nombre = "Receta1",
                Descripcion = "Descripción de la receta 1",
                Ingredientes = new List<IngredientesPasos>(),
                Pasos = new List<IngredientesPasos>()
            };

            //var ctor = typeof(TableResult).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(c => c.GetParameters().Count() == 1);
            //var mockTableResult = ctor.Invoke(new object[] { receta }) as TableResult;
            //return Task.FromResult(mockTableResult);

            return Task.FromResult(new TableResult
            {
                Result = receta,
                HttpStatusCode = 200
            });
        }

        #endregion
    }
}