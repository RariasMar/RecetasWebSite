namespace RecetasWebSite.Domain
{
    /// <summary>
    /// Objeto que contiene la configuración del sitio
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Contiene la cadena de conexión
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    /// <summary>
    /// Objeto que contiene la información necesaria para la cadena de conexión
    /// </summary>
    public class ConnectionStrings
    {
        public string TableStorageCS { get; set; }
    }
}
