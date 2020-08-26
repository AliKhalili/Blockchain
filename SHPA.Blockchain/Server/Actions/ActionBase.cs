using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace SHPA.Blockchain.Server.Actions
{
    public abstract class ActionBase : IAction
    {
        public virtual IActionResult Execute(HttpListenerRequest request)
        {
            throw new System.NotImplementedException();
        }

        public string GetRout()
        {
            return GetType().Name.Substring(0, (int)GetType().Name?.LastIndexOf("Action"));
        }

        protected T ParseQuery<T>(HttpListenerRequest request, string key)
        {
            if (!request.QueryString.HasKeys())
                return default;
            return (T)Convert.ChangeType(request.QueryString[key], typeof(T));
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