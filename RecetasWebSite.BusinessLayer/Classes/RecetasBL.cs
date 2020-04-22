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
        /// <summary>
        /// Obtiene una receta según su identificador
        /// </summary>
        /// <param name="categoria">Categoria de la receta</param>
        /// <param name="id">Identificador de la receta. Es un string.</param>
        /// <returns>Devuelve la receta que coincide con la identificador pasado por parámetros</returns>
        public Receta GetReceta(string categoria, string id)
        {
            RecetasRepositorio repositorio = new RecetasRepositorio();
            return repositorio.GetReceta(categoria, id).Result;
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
            RecetasRepositorio repositorio = new RecetasRepositorio();
            return repositorio.GetRecetas().Result;
        }

        /// <summary>
        /// Inserta una nueva receta en el sistema
        /// </summary>
        /// <param name="receta">Objeto con la receta a insertar</param>
        public void InsertReceta(Receta receta)
        {
            RecetasRepositorio repositorio = new RecetasRepositorio();
            repositorio.InsertReceta(receta).Wait();
        }
    }
}
