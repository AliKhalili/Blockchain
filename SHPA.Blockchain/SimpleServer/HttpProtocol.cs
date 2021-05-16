﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.Extensions.Primitives;

namespace SHPA.Blockchain.SimpleServer
{
    public partial class HttpProtocol : IFeatureCollection
    , IHttpRequestFeature
    //,IHttpResponseFeature
    //,IHttpResponseBodyFeature
    //,IRouteValuesFeature
    //,IEndpointFeature
    //,IHttpRequestIdentifierFeature
    //,IHttpRequestTrailersFeature
    //,IHttpUpgradeFeature
    //,IRequestBodyPipeFeature
    //,IHttpConnectionFeature
    //,IHttpRequestLifetimeFeature
    //,IHttpBodyControlFeature
    //,IHttpMaxRequestBodySizeFeature
    //,IHttpRequestBodyDetectionFeature
    {
        private HttpListenerContext _context;
        private int _featureRevision;

        /// <summary>
        /// HttpConnectionContext
        /// </summary>
        /// <param name="listenerContext"></param>
        public HttpProtocol(HttpListenerContext listenerContext)
        {
            _context = listenerContext;
            _currentIHttpRequestFeature = ToHttpRequestFeature(listenerContext);
        }


        #region IFeatureCollection

        private IEnumerable<KeyValuePair<Type, object>> FastEnumerable()
        {
            if (_currentIHttpRequestFeature != null)
            {
                yield return new KeyValuePair<Type, object>(typeof(IHttpRequestFeature), _currentIHttpRequestFeature);
            }
        }

        public IEnumerator<KeyValuePair<Type, object>> GetEnumerator() => FastEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => FastEnumerable().GetEnumerator();

        public TFeature Get<TFeature>()
        {
            TFeature? feature = default;
            if (typeof(TFeature) == typeof(IHttpRequestFeature))
            {
                feature = Unsafe.As<IHttpRequestFeature?, TFeature?>(ref _currentIHttpRequestFeature);
            }
            return feature;
        }
        public void Set<TFeature>(TFeature instance)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly => false;
        public int Revision => _featureRevision;

        public object? this[Type key]
        {
            get
            {
                object? feature = null;
                if (key == typeof(IHttpRequestFeature))
                {
                    feature = _currentIHttpRequestFeature;
                }
                //else if (key == typeof(IHttpResponseFeature))
                //{
                //    feature = _currentIHttpResponseFeature;
                //}
                //else if (key == typeof(IHttpResponseBodyFeature))
                //{
                //    feature = _currentIHttpResponseBodyFeature;
                //}
                //else if (key == typeof(IRouteValuesFeature))
                //{
                //    feature = _currentIRouteValuesFeature;
                //}
                //else if (key == typeof(IEndpointFeature))
                //{
                //    feature = _currentIEndpointFeature;
                //}
                //else if (key == typeof(IServiceProvidersFeature))
                //{
                //    feature = _currentIServiceProvidersFeature;
                //}
                //else if (key == typeof(IItemsFeature))
                //{
                //    feature = _currentIItemsFeature;
                //}
                //else if (key == typeof(IQueryFeature))
                //{
                //    feature = _currentIQueryFeature;
                //}
                //else if (key == typeof(IRequestBodyPipeFeature))
                //{
                //    feature = _currentIRequestBodyPipeFeature;
                //}
                //else if (key == typeof(IFormFeature))
                //{
                //    feature = _currentIFormFeature;
                //}
                //else if (key == typeof(IHttpAuthenticationFeature))
                //{
                //    feature = _currentIHttpAuthenticationFeature;
                //}
                //else if (key == typeof(IHttpRequestIdentifierFeature))
                //{
                //    feature = _currentIHttpRequestIdentifierFeature;
                //}
                //else if (key == typeof(IHttpConnectionFeature))
                //{
                //    feature = _currentIHttpConnectionFeature;
                //}
                //else if (key == typeof(ISessionFeature))
                //{
                //    feature = _currentISessionFeature;
                //}
                //else if (key == typeof(IResponseCookiesFeature))
                //{
                //    feature = _currentIResponseCookiesFeature;
                //}
                //else if (key == typeof(IHttpRequestTrailersFeature))
                //{
                //    feature = _currentIHttpRequestTrailersFeature;
                //}
                //else if (key == typeof(IHttpResponseTrailersFeature))
                //{
                //    feature = _currentIHttpResponseTrailersFeature;
                //}
                //else if (key == typeof(ITlsConnectionFeature))
                //{
                //    feature = _currentITlsConnectionFeature;
                //}
                //else if (key == typeof(IHttpUpgradeFeature))
                //{
                //    feature = _currentIHttpUpgradeFeature;
                //}
                //else if (key == typeof(IHttpWebSocketFeature))
                //{
                //    feature = _currentIHttpWebSocketFeature;
                //}
                //else if (key == typeof(IHttp2StreamIdFeature))
                //{
                //    feature = _currentIHttp2StreamIdFeature;
                //}
                //else if (key == typeof(IHttpRequestLifetimeFeature))
                //{
                //    feature = _currentIHttpRequestLifetimeFeature;
                //}
                //else if (key == typeof(IHttpMaxRequestBodySizeFeature))
                //{
                //    feature = _currentIHttpMaxRequestBodySizeFeature;
                //}
                //else if (key == typeof(IHttpMinRequestBodyDataRateFeature))
                //{
                //    feature = _currentIHttpMinRequestBodyDataRateFeature;
                //}
                //else if (key == typeof(IHttpMinResponseDataRateFeature))
                //{
                //    feature = _currentIHttpMinResponseDataRateFeature;
                //}
                //else if (key == typeof(IHttpBodyControlFeature))
                //{
                //    feature = _currentIHttpBodyControlFeature;
                //}
                //else if (key == typeof(IHttpRequestBodyDetectionFeature))
                //{
                //    feature = _currentIHttpRequestBodyDetectionFeature;
                //}
                //else if (key == typeof(IHttpResetFeature))
                //{
                //    feature = _currentIHttpResetFeature;
                //}

                return feature;
            }

            set
            {
                _featureRevision++;

                if (key == typeof(IHttpRequestFeature))
                {
                    _currentIHttpRequestFeature = (IHttpRequestFeature?)value;
                }
                //else if (key == typeof(IHttpResponseFeature))
                //{
                //    _currentIHttpResponseFeature = (IHttpResponseFeature?)value;
                //}
                //else if (key == typeof(IHttpResponseBodyFeature))
                //{
                //    _currentIHttpResponseBodyFeature = (IHttpResponseBodyFeature?)value;
                //}
                //else if (key == typeof(IRouteValuesFeature))
                //{
                //    _currentIRouteValuesFeature = (IRouteValuesFeature?)value;
                //}
                //else if (key == typeof(IEndpointFeature))
                //{
                //    _currentIEndpointFeature = (IEndpointFeature?)value;
                //}
                //else if (key == typeof(IServiceProvidersFeature))
                //{
                //    _currentIServiceProvidersFeature = (IServiceProvidersFeature?)value;
                //}
                //else if (key == typeof(IItemsFeature))
                //{
                //    _currentIItemsFeature = (IItemsFeature?)value;
                //}
                //else if (key == typeof(IQueryFeature))
                //{
                //    _currentIQueryFeature = (IQueryFeature?)value;
                //}
                //else if (key == typeof(IRequestBodyPipeFeature))
                //{
                //    _currentIRequestBodyPipeFeature = (IRequestBodyPipeFeature?)value;
                //}
                //else if (key == typeof(IFormFeature))
                //{
                //    _currentIFormFeature = (IFormFeature?)value;
                //}
                //else if (key == typeof(IHttpAuthenticationFeature))
                //{
                //    _currentIHttpAuthenticationFeature = (IHttpAuthenticationFeature?)value;
                //}
                //else if (key == typeof(IHttpRequestIdentifierFeature))
                //{
                //    _currentIHttpRequestIdentifierFeature = (IHttpRequestIdentifierFeature?)value;
                //}
                //else if (key == typeof(IHttpConnectionFeature))
                //{
                //    _currentIHttpConnectionFeature = (IHttpConnectionFeature?)value;
                //}
                //else if (key == typeof(ISessionFeature))
                //{
                //    _currentISessionFeature = (ISessionFeature?)value;
                //}
                //else if (key == typeof(IResponseCookiesFeature))
                //{
                //    _currentIResponseCookiesFeature = (IResponseCookiesFeature?)value;
                //}
                //else if (key == typeof(IHttpRequestTrailersFeature))
                //{
                //    _currentIHttpRequestTrailersFeature = (IHttpRequestTrailersFeature?)value;
                //}
                //else if (key == typeof(IHttpResponseTrailersFeature))
                //{
                //    _currentIHttpResponseTrailersFeature = (IHttpResponseTrailersFeature?)value;
                //}
                //else if (key == typeof(ITlsConnectionFeature))
                //{
                //    _currentITlsConnectionFeature = (ITlsConnectionFeature?)value;
                //}
                //else if (key == typeof(IHttpUpgradeFeature))
                //{
                //    _currentIHttpUpgradeFeature = (IHttpUpgradeFeature?)value;
                //}
                //else if (key == typeof(IHttpWebSocketFeature))
                //{
                //    _currentIHttpWebSocketFeature = (IHttpWebSocketFeature?)value;
                //}
                //else if (key == typeof(IHttp2StreamIdFeature))
                //{
                //    _currentIHttp2StreamIdFeature = (IHttp2StreamIdFeature?)value;
                //}
                //else if (key == typeof(IHttpRequestLifetimeFeature))
                //{
                //    _currentIHttpRequestLifetimeFeature = (IHttpRequestLifetimeFeature?)value;
                //}
                //else if (key == typeof(IHttpMaxRequestBodySizeFeature))
                //{
                //    _currentIHttpMaxRequestBodySizeFeature = (IHttpMaxRequestBodySizeFeature?)value;
                //}
                //else if (key == typeof(IHttpMinRequestBodyDataRateFeature))
                //{
                //    _currentIHttpMinRequestBodyDataRateFeature = (IHttpMinRequestBodyDataRateFeature?)value;
                //}
                //else if (key == typeof(IHttpMinResponseDataRateFeature))
                //{
                //    _currentIHttpMinResponseDataRateFeature = (IHttpMinResponseDataRateFeature?)value;
                //}
                //else if (key == typeof(IHttpBodyControlFeature))
                //{
                //    _currentIHttpBodyControlFeature = (IHttpBodyControlFeature?)value;
                //}
                //else if (key == typeof(IHttpRequestBodyDetectionFeature))
                //{
                //    _currentIHttpRequestBodyDetectionFeature = (IHttpRequestBodyDetectionFeature?)value;
                //}
                //else if (key == typeof(IHttpResetFeature))
                //{
                //    _currentIHttpResetFeature = (IHttpResetFeature?)value;
                //}
            }
        }
        #endregion

        #region IHttpRequestFeature

        private IHttpRequestFeature ToHttpRequestFeature(HttpListenerContext context)
        {
            var httpRequestFeature = new HttpRequestFeature
            {
                Body = context.Request.InputStream,
                Headers = new HeaderDictionary(context.Request.Headers.Count),
                Method = context.Request.HttpMethod,
                Path = context.Request.RawUrl,
                Protocol = context.Request.ProtocolVersion.ToString(),
                PathBase = context.Request.RawUrl,
                QueryString = context.Request.QueryString.ToString(),
                Scheme = "http"
            };
            foreach (var requestHeaderKey in context.Request.Headers.AllKeys)
            {
                httpRequestFeature.Headers.Add(requestHeaderKey, new StringValues(context.Request.Headers[requestHeaderKey]));
            }
            return httpRequestFeature;
        }
        internal protected IHttpRequestFeature? _currentIHttpRequestFeature;
        public string Protocol { get; set; }
        public string Scheme { get; set; }
        public string Method { get; set; }
        public string PathBase { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string RawTarget { get; set; }
        public IHeaderDictionary Headers { get; set; }
        public Stream Body { get; set; }
        #endregion

    }
}