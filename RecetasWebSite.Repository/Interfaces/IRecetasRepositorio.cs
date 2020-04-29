using RecetasWebSite.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecetasWebSite.Repository
{
    /// <summary>
    /// Interfaz que contiene las operaciones que se van a realizar con las recetas
    /// </summary>
    public interface IRecetasRepositorio
    {
        /// <summary>
        /// Elimina una receta
        /// </summary>
        /// <param name="categoria">Categoria de la receta</param>
        /// <param name="id">Identificador de la receta</param>
        /// <param name="receta">La receta en sí</param>
        /// <returns>Devuelve un booleano indicando si la operación ha ido bien o mal</returns>
        Task<bool> DeleteReceta(string categoria, string id, Receta receta);

        /// <summary>
        /// Obtiene una receta según su identificador
        /// </summary>
        /// <param name="categoria">Categoria de la receta</param>
        /// <param name="id">Identificador de la receta. Es un string.</param>
        /// <returns>Devuelve la receta que coincide con la identificador pasado por parámetros</returns>
        Task<Receta> GetReceta(string categoria, string id);

        /// <summary>
        /// Busca recetas basado en unos criterios de búsqueda
        /// </summary>
        /// <returns>Devuelve un listado con todas las recetas que satisfacen los criteriosde búsqueda</returns>
        Task<List<Receta>> GetRecetaBy();

        /// <summary>
        /// Obtiene todas las recetas
        /// </summary>
        /// <returns>Devuelve un listado con todas las recetas</returns>
        Task<List<Receta>> GetRecetas();

        /// <summary>
        /// Inserta una nueva receta en el sistema
        /// </summary>
        /// <param name="receta">Objeto con la receta a insertar</param>
        /// <returns>Devuelve un booleano indicando si la operación ha ido bien o mal</returns>
        Task<bool> InsertReceta(Receta receta);
    }
}
