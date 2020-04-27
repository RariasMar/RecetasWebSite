using Microsoft.Extensions.Options;
using RecetasWebSite.BusinessLayer.Interfaces;
using RecetasWebSite.Domain;
using RecetasWebSite.Repository;
using System;
using System.Collections.Generic;

namespace RecetasWebSite.BusinessLayer.Classes
{
    /// <summary>
    /// Clase que contiene las operaciones que se van a realizar con las recetas
    /// </summary>
    public class RecetasBL : IRecetasBL
    {
        private readonly RecetasRepositorio repositorio;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public RecetasBL(IOptions<Configuration> options) : base()
        {
            this.repositorio = new RecetasRepositorio(options.Value);
        }

        /// <summary>
        /// Elimina una receta
        /// </summary>
        /// <param name="categoria">Categoria de la receta</param>
        /// <param name="id">Identificador de la receta</param>
        /// <param name="receta">La receta en sí</param>
        /// <returns>Devuelve un booleano indicando si la operación ha ido bien o mal</returns>
        public bool DeleteReceta(string categoria, string id, Receta receta)
        {
            return this.repositorio.DeleteReceta(categoria, id, receta).Result;
        }

        /// <summary>
        /// Obtiene una receta según su identificador
        /// </summary>
        /// <param name="categoria">Categoria de la receta</param>
        /// <param name="id">Identificador de la receta. Es un string.</param>
        /// <returns>Devuelve la receta que coincide con la identificador pasado por parámetros</returns>
        public Receta GetReceta(string categoria, string id)
        {
            return this.repositorio.GetReceta(categoria, id).Result;
        }

        /// <summary>
        /// Busca recetas basado en unos criterios de búsqueda
        /// </summary>
        /// <returns>Devuelve un listado con todas las recetas que satisfacen los criteriosde búsqueda</returns>
        public List<Receta> GetRecetaBy()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene todas las recetas
        /// </summary>
        /// <returns>Devuelve un listado con todas las recetas</returns>
        public List<Receta> GetRecetas()
        {
            return this.repositorio.GetRecetas().Result;
        }

        /// <summary>
        /// Inserta una nueva receta en el sistema
        /// </summary>
        /// <param name="receta">Objeto con la receta a insertar</param>
        /// <returns>Devuelve un booleano indicando si la operación ha ido bien o mal</returns>
        public bool InsertReceta(Receta receta)
        {
            return this.repositorio.InsertReceta(receta).Result;
        }
    }
}
