using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecetasWebSite.Models;
using System.Diagnostics;

namespace RecetasWebSite.Controllers
{
    /// <summary>
    /// Controlador para gestionar los errores ocurridos en la aplicación
    /// </summary>
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="logger"></param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        } 

        /// <summary>
        /// Redirige a la pantalla de error cuando ocurre alguno
        /// </summary>
        /// <returns>LA vista error</returns>
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}