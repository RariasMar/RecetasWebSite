using System;
using System.Collections.Generic;

namespace RecetasWebSite.Domain
{
    /// <summary>
    /// Objeto que contiene las propiedades de una receta
    /// </summary>
    public class Receta
    {
        /// <summary>
        /// Identificador de la receta
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nombre de la receta
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Iamgen principal de la receta
        /// </summary>
        public string ImagenPrincipal { get; set; }

        /// <summary>
        /// Descripción general de la receta
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Etiquetas de la receta para catagorizarla
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Listado con los ingredientes de la receta
        /// </summary>
        public List<IngredientesPasos> Ingredientes { get; set; }

        /// <summary>
        /// Listado con los pasos a seguir
        /// </summary>
        public List<IngredientesPasos> Pasos { get; set; }

        /// <summary>
        /// Url del video de la receta
        /// </summary>
        public string Video { get; set; }

        /// <summary>
        /// Url de la fuente de dónde hemos obtenido la receta
        /// </summary>
        public string Fuente { get; set; }

        /// <summary>
        /// Lista de imagenes con la creación final
        /// </summary>
        public List<string> Imagenes { get; set; }
    }
}
