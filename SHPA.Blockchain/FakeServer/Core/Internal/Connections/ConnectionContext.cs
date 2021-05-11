using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http.Features;

namespace SHPA.Blockchain.FakeServer.Core.Internal.Connections
{
    /// <summary>
    /// Encapsulates all information about an individual connection.
    /// </summary>
    public class ConnectionContext
    {
        private HttpListenerContext _httpListenerContext;

        public ConnectionContext(HttpListenerContext context)
        {
            _httpListenerContext = context;
        }

        /// <summary>
        /// Gets or sets a unique identifier to represent this connection in trace logs.
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// Gets the collection of features provided by the server and middleware available on this connection.
        /// </summary>
        public IFeatureCollection Features { get; }

        /// <summary>
        /// Gets or sets a key/value collection that can be used to share data within the scope of this connection.
        /// </summary>
        public IDictionary<object, object?> Items { get; set; }
    }
}