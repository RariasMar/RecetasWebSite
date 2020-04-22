using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecetasWebSite.BusinessLayer.Classes;
using RecetasWebSite.BusinessLayer.Interfaces;
using RecetasWebSite.Domain;
using RecetasWebSite.Models;

namespace RecetasWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecetasBL _recetasService;

        public HomeController(ILogger<HomeController> logger, IRecetasBL recetasService)
        {
            _logger = logger;
            _recetasService = recetasService;
        }

        public IActionResult Index()
        {
            List<Receta> recetas = this._recetasService.GetRecetas();
            return View(recetas);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
