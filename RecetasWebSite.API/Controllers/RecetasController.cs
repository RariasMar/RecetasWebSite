using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecetasWebSite.Domain;
using RecetasWebSite.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecetasWebSite.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecetasController : ControllerBase
    {
        /// <summary>
        /// Servicio de recetas
        /// </summary>
        private readonly IRecetasRepositorio recetasService;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="recetasService"></param>
        /// <param name="logger"></param>
        public RecetasController(IRecetasRepositorio recetasService)
        {
            this.recetasService = recetasService;
        }

        /// <summary>
        /// Devuelve todas las recetas del sistema
        /// </summary>
        /// <returns>Listado con todas las recetas</returns>
        [HttpGet]
        public async Task<ActionResult<List<Receta>>> GetRecetas()
        {
            List<Receta> recetas = await this.recetasService.GetRecetas();
            return recetas;
        }

        /// <summary>
        /// Devuelve una receta por su categoria e id
        /// </summary>
        /// <param name="categoria">Categoria de la receta</param>
        /// <param name="id">Id de la receta</param>
        /// <returns>Una receta</returns>
        [HttpGet("{categoria}/{id}")]
        public async Task<ActionResult<Receta>> GetReceta(string categoria, string id)
        {
            Receta receta = await this.recetasService.GetReceta(categoria, id);
            if (receta == null)
            {
                return this.NotFound();
            }

            return receta;
        }
    }
}
