using RecetasWebSite.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecetasWebSite.BusinessLayer.Interfaces
{
    /// <summary>
    /// Interfaz que contiene las operaciones que se van a realizar con las recetas
    /// </summary>
    public interface IRecetasBL
    {
        /// <summary>
        /// Obtiene una receta según su identificador
        /// </summary>
        /// <param name="categoria">Categoria de la receta</param>
        /// <param name="id">Identificador de la receta. Es un string.</param>
        /// <returns>Devuelve la receta que coincide con la identificador pasado por parámetros</returns>
        Receta GetReceta(string categoria, string id);

        /// <summary>
        /// Busca recetas basado en unos criterios de búsqueda
        /// </summary>
        /// <returns>Devuelve un listado con todas las recetas que satisfacen los criteriosde búsqueda</returns>
        List<Receta> GetRecetaBy();

        /// <summary>
        /// Obtiene todas las recetas
        /// </summary>
        /// <returns>Devuelve un listado con todas las recetas</returns>
        List<Receta> GetRecetas();

        /// <summary>
        /// Inserta una nueva receta en el sistema
        /// </summary>
        /// <param name="receta">Objeto con la receta a insertar</param>
        void InsertReceta(Receta receta);
    }
}
