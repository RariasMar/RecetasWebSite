using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RecetasWebSite.Controllers;
using RecetasWebSite.Models;
using System;

namespace RecetasWebSite.Test.Unit
{
    /// <summary>
    /// Clase de pruebas del controlador Error
    /// </summary>
    [TestFixture]
    public class ErrorControllerTest
    {
        #region Campos Privados

        private ErrorController controller;
        private Mock<ILogger<ErrorController>> logger;

        #endregion

        #region Setup

        /// <summary>
        /// Configura los objetos necesarios
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            logger = new Mock<ILogger<ErrorController>>();
            controller = new ErrorController(logger.Object);

            //Genera un HttpContext fake con un TraceIdentifier
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.TraceIdentifier = (It.IsAny<Guid>()).ToString("B");
        }

        #endregion

        #region Error

        /// <summary>
        /// Comprueba la acción Index del controlador Home con una lista de recetas rellena
        /// </summary>
        [Test]
        public void Error_ReturnsAViewResult_WithAnErrorViewModel()
        {
            ViewResult result = (ViewResult)controller.Error();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsAssignableFrom<ErrorViewModel>(result.ViewData.Model);
            Assert.IsNotEmpty(((ErrorViewModel)result.ViewData.Model).RequestId);
        }

        #endregion
    }
}