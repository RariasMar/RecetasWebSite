using Microsoft.AspNetCore.Mvc;
using RecetasWebSite.Domain;
using RecetasWebSite.Repository;
using System.Collections.Generic;

namespace RecetasWebSite.Controllers
{
    /// <summary>
    /// Controlador para gestionar lo relacionados a las recetas
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IRecetasRepositorio recetasService;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="recetasService"></param>
        public HomeController(IRecetasRepositorio recetasService)
        {
            this.recetasService = recetasService;
        }

        /// <summary>
        /// Obtiene un listado con todas las recetas
        /// </summary>
        /// <returns>Devuelve la vista Index</returns>
        public IActionResult Index()
        {
            List<Receta> recetas = this.recetasService.GetRecetas().Result;
            return View(recetas);
        }

        /// <summary>
        /// Navega a los detalles de la recetas
        /// </summary>
        /// <param name="categoria">Categoría de la receta</param>
        /// <param name="id">Identificador de la receta</param>
        /// <returns>Devuelve la vista Detalles</returns>
        public IActionResult Details(string categoria, string id)
        {
            Receta receta = this.recetasService.GetReceta(categoria, id).Result;
            return View(receta);
        }
    }
}
