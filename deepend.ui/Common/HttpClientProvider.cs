using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace deepend.ui.Common
{
    public class HttpClientProvider :IHttpClientProvider
    {
        private readonly static HttpClient httpClient;

        static HttpClientProvider()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"])
            };

        }

        public HttpClient GetHttpClient()
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }
    }
}