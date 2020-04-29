using Microsoft.AspNetCore.Mvc;
using RecetasWebSite.Domain;
using RecetasWebSite.Repository;
using System.Collections.Generic;

namespace RecetasWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecetasRepositorio recetasService;

        public HomeController(IRecetasRepositorio recetasService)
        {
            this.recetasService = recetasService;
        }

        public IActionResult Index()
        {
            List<Receta> recetas = this.recetasService.GetRecetas().Result;
            return View(recetas);
        }
        public IActionResult Details(string categoria, string id)
        {
            Receta receta = this.recetasService.GetReceta(categoria, id).Result;
            return View(receta);
        }
    }
}
