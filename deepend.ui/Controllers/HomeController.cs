using deepend.entity.Response;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using deepend.ui.Common;

namespace deepend.ui.Controllers
{
    public class HomeController : Controller
    {
        private IHttpClientProvider httpClientProvider;
        private const string GetAllChequesRoute = "api/v1/cheques/all";

        public HttpClient HttpClient
        {
            get
            {
                return httpClientProvider.GetHttpClient();
            }
        }
        public HomeController(IHttpClientProvider httpClientProvider)
        {
            this.httpClientProvider = httpClientProvider;
        }


        public ActionResult Index()
        {
            var result = GetAllCheques();
            if (result != null)
            {
                result=result.OrderByDescending(ch => ch.DateTime).ToList();
                return View("Index", result);

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Something went wrong");
            }

        }


        private List<ChequeResponse> GetAllCheques()
        {
            List<ChequeResponse> chequesResponse = new List<ChequeResponse>();


            HttpResponseMessage message = HttpClient.GetAsync(GetAllChequesRoute).GetAwaiter().GetResult();
            if (message.IsSuccessStatusCode)
            {
                chequesResponse = message.Content.ReadAsAsync<List<ChequeResponse>>().GetAwaiter().GetResult();
                return chequesResponse;

            }
            else
            {
                return null;
            }



        }

    }
}