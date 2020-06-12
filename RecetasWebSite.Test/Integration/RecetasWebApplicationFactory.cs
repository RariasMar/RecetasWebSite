using Microsoft.AspNetCore.Mvc.Testing;

namespace RecetasWebSite.Test.Integration
{
    /// <summary>
    /// Factoria utilizada al realizar los test de integración
    /// </summary>
    class RecetasWebApplicationFactory : WebApplicationFactory<RecetasWebSite.Startup>
    {
    }
}
