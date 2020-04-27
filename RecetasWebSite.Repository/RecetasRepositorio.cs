using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using RecetasWebSite.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecetasWebSite.Repository
{
    public class RecetasRepositorio
    {
        private CloudStorageAccount cuentaAlmacenamiento;
        private CloudTableClient cliente;
        private CloudTable recetasTabla;
        
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public RecetasRepositorio(Configuration config)
        {
            if (!CloudStorageAccount.TryParse(config.ConnectionString, out cuentaAlmacenamiento))
            {
                return;
            }

            cliente = cuentaAlmacenamiento.CreateCloudTableClient();
            recetasTabla = cliente.GetTableReference("Recetas");
        }

        /// <summary>
        /// Elimina una receta
        /// </summary>
        /// <param name="categoria">Categoria de la receta</param>
        /// <param name="id">Identificador de la receta</param>
        /// <param name="receta">La receta en sí</param>
        /// <returns>Devuelve un booleano indicando si la operación ha ido bien o mal</returns>
        public async Task<bool> DeleteReceta(string categoria, string id, Receta receta)
        {
            var resultado = await recetasTabla.ExecuteAsync(TableOperation.Retrieve<RecetaEntity>(categoria, id));
            RecetaEntity recetaEntity = (RecetaEntity)resultado.Result;
            resultado = await recetasTabla.ExecuteAsync(TableOperation.Delete(recetaEntity));

            return resultado.HttpStatusCode == 204;
        }

        /// <summary>
        /// Obtiene una receta según su identificador
        /// </summary>
        /// <param name="categoria">Categoria de la receta</param>
        /// <param name="id">Identificador de la receta. Es un string.</param>
        /// <returns>Devuelve la receta que coincide con la identificador pasado por parámetros</returns>
        public async Task<Receta> GetReceta(string categoria, string id)
        {
            var resultado = await recetasTabla.ExecuteAsync(TableOperation.Retrieve<RecetaEntity>(categoria, id));
            RecetaEntity recetaEntity = (RecetaEntity)resultado.Result;
            return JsonConvert.DeserializeObject<Receta>(recetaEntity.Receta);
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
        public async Task<List<Receta>> GetRecetas()
        {
            List<Receta> recetas = new List<Receta>();
            TableQuery<RecetaEntity> recetasQuery = new TableQuery<RecetaEntity>();
            TableContinuationToken token = null;

            do
            {
                TableQuerySegment<RecetaEntity> resultado = await recetasTabla.ExecuteQuerySegmentedAsync(recetasQuery, new TableContinuationToken());
                token = resultado.ContinuationToken;
                foreach (RecetaEntity receta in resultado)
                {
                    recetas.Add(JsonConvert.DeserializeObject<Receta>(receta.Receta));
                }
            } while (token != null);

            return recetas;
        }

        /// <summary>
        /// Inserta una nueva receta en el sistema
        /// </summary>
        /// <param name="receta">Objeto con la receta a insertar</param>
        /// <returns>Devuelve un booleano indicando si la operación ha ido bien o mal</returns>
        public async Task<bool> InsertReceta(Receta receta)
        {
            RecetaEntity recetaEntity = new RecetaEntity(receta);
            TableOperation insertOrMerge = TableOperation.InsertOrReplace(recetaEntity);
            var resultado = await recetasTabla.ExecuteAsync(insertOrMerge);

            return resultado.HttpStatusCode == 204;
        }
    }
}
