using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using RecetasWebSite.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecetasWebSite.Repository
{
    public class RecetasRepositorio : IRecetasRepositorio
    {
        #region Campos Privados

        //Campos privados que se utilizan en la clase
        private readonly CloudTable recetasTabla;
        private readonly ILogger<RecetasRepositorio> logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public RecetasRepositorio(ILogger<RecetasRepositorio> log, CloudTable recetasTabla)
        {
            this.logger = log;
            this.recetasTabla = recetasTabla;
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public RecetasRepositorio(IOptions<Configuration> options, ILogger<RecetasRepositorio> log)
        {
            CloudStorageAccount cuentaAlmacenamiento;
            CloudTableClient cliente;

            this.logger = log;

            if (!CloudStorageAccount.TryParse(options.Value.ConnectionStrings.TableStorageCS, out cuentaAlmacenamiento))
            {
                throw new StorageException($"Formato de connection string erroneo.\nConnection String: {options.Value.ConnectionStrings.TableStorageCS}");
            }

            cliente = cuentaAlmacenamiento.CreateCloudTableClient();
            recetasTabla = cliente.GetTableReference("Recetas");
            if (!recetasTabla.ExistsAsync().Result)
            {
                throw new StorageException("La tabla Recetas no existe en la base de datos");
            }
        }

        #endregion

        #region Métodos Públicos

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

            if (resultado.HttpStatusCode == 404 || resultado.Result == null)
            {
                throw new StorageException($"Receta con Id {id} y categoría {categoria} no encontrada en la base de datos.");
            }
            else
            {
                try
                {
                    RecetaEntity recetaEntity = (RecetaEntity)resultado.Result;
                    return JsonConvert.DeserializeObject<Receta>(recetaEntity.Receta);
                }
                catch (Exception ex)
                {
                    throw new StorageException($"El formato JSON del objeto de base de datos no coincide con el esperado en código.\n Id: {id}\nCategoria: {categoria}\nJson: {resultado.Result}", ex);
                }
            }
        }

        /// <summary>
        /// Busca recetas basado en unos criterios de búsqueda
        /// </summary>
        /// <returns>Devuelve un listado con todas las recetas que satisfacen los criteriosde búsqueda</returns>
        public Task<List<Receta>> GetRecetaBy()
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
            TableContinuationToken token = null;
            int totalCount = 0;

            using (logger.BeginScope("Obteniendo todas las recetas"))
            {
                do
                {
                    TableQuerySegment<RecetaEntity> resultado = await recetasTabla.ExecuteQuerySegmentedAsync<RecetaEntity>(new TableQuery<RecetaEntity>(), new TableContinuationToken());
                    token = resultado.ContinuationToken;
                    totalCount += resultado.Results.Count;

                    foreach (RecetaEntity receta in resultado)
                    {
                        try
                        {
                            recetas.Add(JsonConvert.DeserializeObject<Receta>(receta.Receta));
                        }
                        catch (Exception ex)
                        {
                            this.logger.LogWarning(ex, $"El formato JSON del objeto de base de datos no coincide con el esperado en código.\nJson: {receta.Receta}");
                        }
                    }
                } while (token != null);

                if (recetas.Count != totalCount)
                {
                    this.logger.LogWarning($"Existen recetas en el repositorio con un formato de JSON diferente al esperado.\nTotal Esperadp: {totalCount}\nTotal Recibido: {recetas.Count}");
                }
            }

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

        #endregion
    }
}
