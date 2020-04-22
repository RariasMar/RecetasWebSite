using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecetasWebSite.Domain
{
    public class RecetaEntity : TableEntity
    {
        public RecetaEntity()
        {
        }

        public RecetaEntity(Receta receta)
        {
            PartitionKey = receta.Categoria;
            RowKey = receta.Id;
            Receta = JsonConvert.SerializeObject(receta);
        }

        public string Receta { get; set; }
    }
}
