using System;

namespace SHPA.Blockchain.FakeServer.Core
{
    public class FakeServerOptions
    {
        /// <summary>
        /// Enables the Listen options callback to resolve and use services registered by the application during startup.
        /// Typically initialized by UseKestrel().
        /// </summary>
        public IServiceProvider ApplicationServices { get; set; } = default!; // This should typically be set
    }
}