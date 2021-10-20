using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RecetasWebSite.API.Controllers;
using RecetasWebSite.Domain;
using RecetasWebSite.Repository;
using System.Collections.Generic;

namespace RecetasWebSite.API.Test.Unit
{
    /// <summary>
    /// Clase de pruebas del controlador Recetas
    /// </summary>
    [TestFixture]
    public class RecetasControllerTest
    {
        #region Campos Privados

        private Mock<IRecetasRepositorio> recetasRepositorio;
        private RecetasController controller;

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
            controller = new RecetasController(recetasRepositorio.Object);
        }

        #endregion

        #region Obtener todas las recetas

        /// <summary>
        /// Comprueba la acción GetRecetas del controlador Recetas con una lista de recetas rellena
        /// </summary>
        [Test]
        public void GetRecetas_ReturnsAnObjectList_WithAllRecetas()
        {
            ActionResult<List<Receta>> result = controller.GetRecetas().Result;

            // Assert
            Assert.IsInstanceOf<ActionResult<List<Receta>>>(result);
            Assert.IsAssignableFrom<List<Receta>>(result.Value);
            Assert.AreEqual(2, result.Value.Count);
        }

        /// <summary>
        /// Comprueba la acción GetRecetas del controlador Recetas con una lista de recetas vacía
        /// </summary>
        [Test]
        public void GetRecetas_ReturnsAnObjectList_WithNoItems()
        {
            recetasRepositorio.Setup(recetas => recetas.GetRecetas()).ReturnsAsync(new List<Receta>());
            ActionResult<List<Receta>> result = controller.GetRecetas().Result;

            // Assert
            Assert.IsInstanceOf<ActionResult<List<Receta>>>(result);
            Assert.IsAssignableFrom<List<Receta>>(result.Value);
            Assert.AreEqual(0, result.Value.Count);
        }

        #endregion

        #region Detalles de una receta

        /// <summary>
        /// Comprueba la acción GetReceta del controlador Receta
        /// </summary>
        [Test]
        public void GetReceta_ReturnsAnObject_WithOneReceta()
        {
            ActionResult<Receta> result = controller.GetReceta(It.IsAny<string>(), It.IsAny<string>()).Result;

            // Assert
            Assert.IsInstanceOf<ActionResult<Receta>>(result);
            Assert.IsAssignableFrom<Receta>(result.Value);
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