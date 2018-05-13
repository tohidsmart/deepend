using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace deepend.ui.Common
{
    public interface IHttpClientProvider
    {
        HttpClient GetHttpClient();
    }
}