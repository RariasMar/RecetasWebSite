using RecetasWebSite.Domain;
using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace RecetasWebSite.Repository
{
    public class RecetasRepositorio
    {
        private CloudStorageAccount cuentaAlmacenamiento;
        private CloudTableClient cliente;
        private CloudTable recetasTabla;
        private string connectionString = "DefaultEndpointsProtocol=https;AccountName=recetasstrgacc;AccountKey=JuuYh0pQIQnSRBWR51jRLKNfcNo2Q3r3E9KwzHNl0i+zRTn6mNvyTkdV7TmXSBARSKlqcuUQxVTtwVlTUBNUuw==;EndpointSuffix=core.windows.net";

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public RecetasRepositorio()
        {
            if (!CloudStorageAccount.TryParse(connectionString, out cuentaAlmacenamiento))
            {
                return;
            }

            cliente = cuentaAlmacenamiento.CreateCloudTableClient();
            recetasTabla = cliente.GetTableReference("Recetas");
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
            RecetaEntity recetaEntity = resultado.Result as RecetaEntity;
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
        public async Task InsertReceta(Receta receta)
        {
            RecetaEntity recetaEntity = new RecetaEntity(receta);
            TableOperation insertOrMerge = TableOperation.InsertOrReplace(recetaEntity);
            await recetasTabla.ExecuteAsync(insertOrMerge);
        }
    }
}
