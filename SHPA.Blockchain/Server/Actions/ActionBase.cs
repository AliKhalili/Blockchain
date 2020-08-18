using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace SHPA.Blockchain.Server.Actions
{
    public abstract class ActionBase : IAction
    {
        public virtual IActionResult Execute(HttpListenerRequest request)
        {
            throw new System.NotImplementedException();
        }

        protected T ParseBody<T>(HttpListenerRequest request) where T : class
        {
            if (!request.HasEntityBody || request.HttpMethod != "POST")
            {
                return null;
            }

            using Stream body = request.InputStream;
            using StreamReader reader = new StreamReader(body, request.ContentEncoding);
            var content = reader.ReadToEnd();
            if (!string.IsNullOrEmpty(content))
            {
                var input = JsonConvert.DeserializeObject<T>(content);
                return input;
            }

            return null;
        }
    }
}