using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;

namespace SHPA.Blockchain.Client
{
    public class RestClient
    {
        private readonly Url _baseUrl;
        private readonly Dictionary<string, string> _defaultHeader;
        private readonly int _timeOut;
        private readonly HttpClient _client;

        private string _method;
        private Dictionary<string, string> _query;
        private string _body;
        private RestClient(Uri baseUrl, int timeOut = 60)
        {
            _defaultHeader = new Dictionary<string, string>
            {
                {"Content-Type", "application/json"},
                {"Accept", "application/json"},
                {"UserAgent","Rest.Client.Agent"}
            };
            _timeOut = timeOut;
            _client = new HttpClient();
            AddDefaultHeard();
            _query = new Dictionary<string, string>();
            _body = null;
            _method = string.Empty;
        }

        private void AddDefaultHeard()
        {
            foreach (var header in _defaultHeader)
            {
                _client.DefaultRequestHeaders.Add(header.Key, new[] { header.Value });
            }
        }

        private string GetQuery()
        {
            if (_query.Any())
            {
                return $"?{_query.Select(x => $"{x.Key}={x.Value}").Aggregate((c, n) => $"{c}&{n}")}";
            }

            return string.Empty;
        }

        private StringContent GetBody()
        {
            if (_body != null)
            {
                return new StringContent(_body, Encoding.UTF8, "application/json");
            }
            return new StringContent(string.Empty);
        }
        public static RestClient Make(Uri baseUrl, int timeOut = 60)
        {
            return new RestClient(baseUrl, timeOut);
        }

        public RestClient Get()
        {
            _method = "GET";
            return this;
        }

        public RestClient Post()
        {
            _method = "POST";
            return this;
        }

        public RestClient AddBody(object body)
        {
            _body = JsonConvert.SerializeObject(body);
            return this;
        }

        public RestClient AddQuery(string key, string value)
        {
            _query.Add(key, value);
            return this;
        }

        public RestClient AddHeader(string key, string value)
        {
            _client.DefaultRequestHeaders.Add(key, value);
            return this;
        }

        public T Execute<T>(string url) where T : class
        {
            if (_method == "GET")
            {
                HttpResponseMessage response = _client.GetAsync($"{_baseUrl}/{url}{GetQuery()}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                return null;
            }
            else if (_method == "POST")
            {
                HttpResponseMessage response = _client.PostAsync($"{_baseUrl}/{url}{GetQuery()}", GetBody()).Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return null;
        }

    }
}