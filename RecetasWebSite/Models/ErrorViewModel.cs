using System;

namespace RecetasWebSite.Models
{
    /// <summary>
    /// Objeto con los datos del error
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Identificador de la petici�n que falla
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Booleano para mostrar u ocultar el identificador de la petici�n
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
