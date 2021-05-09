using System.Threading.Tasks;

namespace SHPA.Blockchain.Server.Http
{
    /// <summary>
    /// A function that can process an HTTP request.
    /// <br></br><b>***entire codes were copied from the <c>Microsoft.AspNetCore</c> source code.***</b>
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> for the request.</param>
    /// <returns>A task that represents the completion of request processing.</returns>
    public delegate Task RequestDelegate(HttpContext context);
}