using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RecetasWebSite.Controllers;
using RecetasWebSite.Domain;
using RecetasWebSite.Repository;
using System.Collections.Generic;

namespace RecetasWebSite.Test.Unit
{
    /// <summary>
    /// Clase de pruebas del controlador Home
    /// </summary>
    [TestFixture]
    public class HomeControllerTest
    {
        #region Campos Privados

        private Mock<IRecetasRepositorio> recetasRepositorio;
        private HomeController controller;

        #endregion

        #region Setup

        /// <summary>
        /// Configura los objetos necesarios
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            recetasRepositorio = new Mock<IRecetasRepositorio>();
            recetasRepositorio.Setup(recetas => recetas.GetRecetas()).ReturnsAsync(GetTestRecetas());
            recetasRepositorio.Setup(recetas => recetas.GetReceta(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(GetTestRecetas()[0]);
            controller = new HomeController(recetasRepositorio.Object);
        }

        #endregion

        #region Obtener todas las recetas

        /// <summary>
        /// Comprueba la acción Index del controlador Home con una lista de recetas rellena
        /// </summary>
        [Test]
        public void Index_ReturnsAViewResult_WithAListOfRecetas()
        {
            ViewResult result = (ViewResult)controller.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsAssignableFrom<List<Receta>>(result.ViewData.Model);
            Assert.AreEqual(2, ((List<Receta>)result.ViewData.Model).Count);
        }

        /// <summary>
        /// Comprueba la acción Index del controlador Home con una lista de recetas vacía
        /// </summary>
        [Test]
        public void Index_ReturnsAViewResult_WithAListOfRecetasWithNoItems()
        {
            recetasRepositorio.Setup(recetas => recetas.GetRecetas()).ReturnsAsync(new List<Receta>());
            ViewResult result = (ViewResult)controller.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsAssignableFrom<List<Receta>>(result.ViewData.Model);
            Assert.AreEqual(0, ((List<Receta>)result.ViewData.Model).Count);
        }

        #endregion

        #region Detalles de una receta

        /// <summary>
        /// Comprueba la acción Details del controlador Home
        /// </summary>
        [Test]
        public void Details_ReturnsAViewResult_WithOneReceta()
        {
            ViewResult result = (ViewResult)controller.Details(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsAssignableFrom<Receta>(result.ViewData.Model);
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Crea un listado de recetas de prueba
        /// </summary>
        /// <returns>Datos de prueba</returns>
        private List<Receta> GetTestRecetas()
        {
            List<Receta> recetas = new List<Receta>();
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

            recetas.Add(receta1);
            recetas.Add(receta2);

            return recetas;
        }

        #endregion
    }
}