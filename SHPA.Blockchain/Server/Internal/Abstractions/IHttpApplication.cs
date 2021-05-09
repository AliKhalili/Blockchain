﻿using System;
using System.Threading.Tasks;

namespace SHPA.Blockchain.Server
{
    /// <summary>
    /// Represents an application.
    /// <br></br><b>***entire codes were copied from the <c>Microsoft.AspNetCore</c> source code.***</b>
    /// </summary>
    /// <typeparam name="TContext">The context associated with the application.</typeparam>
    public interface IHttpApplication<TContext> where TContext : notnull
    {
        /// <summary>
        /// Create a TContext given a collection of HTTP features.
        /// </summary>
        /// <param name="contextFeatures">A collection of HTTP features to be used for creating the TContext.</param>
        /// <returns>The created TContext.</returns>
        TContext CreateContext(IFeatureCollection contextFeatures);

        /// <summary>
        /// Asynchronously processes an TContext.
        /// </summary>
        /// <param name="context">The TContext that the operation will process.</param>
        Task ProcessRequestAsync(TContext context);

        /// <summary>
        /// Dispose a given TContext.
        /// </summary>
        /// <param name="context">The TContext to be disposed.</param>
        /// <param name="exception">The Exception thrown when processing did not complete successfully, otherwise null.</param>
        void DisposeContext(TContext context, Exception? exception);
    }
}