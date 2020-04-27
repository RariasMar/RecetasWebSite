using System.Collections.Generic;

namespace RecetasWebSite.Domain
{
    /// <summary>
    /// Objeto que contiene las propiedades de un paso
    /// </summary>
    public class IngredientesPasos
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public IngredientesPasos() {
            this.Descripcion = new List<string>();
        }

        /// <summary>
        /// Imagen asociada al paso
        /// </summary>
        public string Cabecera { get; set; }

        /// <summary>
        /// Descripción del paso
        /// </summary>
        public List<string> Descripcion { get; set; }
    }
}
