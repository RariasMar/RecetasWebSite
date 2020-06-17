using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace RecetasWebSite.Domain
{
    /// <summary>
    /// Clase entity utilizada para mapear los datos de Table Storage
    /// </summary>
    public class RecetaEntity : TableEntity
    {
        /// <summary>
        /// Constructor vacío de la clase
        /// </summary>
        public RecetaEntity()
        {
        }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        public RecetaEntity(Receta receta)
        {
            PartitionKey = receta.Categoria;
            RowKey = receta.Id;
            Receta = JsonConvert.SerializeObject(receta);
        }

        /// <summary>
        /// String con el json que contiene la receta
        /// </summary>
        public string Receta { get; set; }
    }
}
