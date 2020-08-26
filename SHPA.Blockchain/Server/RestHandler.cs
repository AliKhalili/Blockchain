using SHPA.Blockchain.Server.ActionResult;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SHPA.Blockchain.Server
{
    public class RestHandler : IRequestHandler
    {
        private readonly IActionFactory _actionFactory;

        public RestHandler(IActionFactory actionFactory)
        {
            _actionFactory = actionFactory;
        }
        public async Task HandleAsync(Task<HttpListenerContext> taskListener)
        {
            var context = await taskListener;
            HttpListenerRequest request = context.Request;
            IActionResult result;
            try
            {
                result = _actionFactory.Create(request).Execute(request);
            }
            catch (Exception e)
            {
                result = new ActionResult<object>().AddErrors(new[] { e.InnerException?.Message ?? e.Message }, HttpStatusCode.InternalServerError);
            }

            HttpListenerResponse response = context.Response;
            response.ContentType = "application/json";

            string content;
            (response.StatusCode, content) = result.GetResult();
            if (!string.IsNullOrEmpty(content))
            {
                var byteResponse = Encoding.UTF8.GetBytes(content);
                response.ContentLength64 = content.Length;
                await context.Response.OutputStream.WriteAsync(byteResponse, 0, byteResponse.Length);
                context.Response.OutputStream.Close();
            }
        }
    }
}