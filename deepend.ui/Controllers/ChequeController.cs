using deepend.entity.Request;
using deepend.entity.Response;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using deepend.ui.Common;
using deepend.ui.App_Start;

namespace deepend.ui.Controllers
{
    public class ChequeController : Controller
    {
        private IHttpClientProvider httpClientProvider;
        private const string AddChequeRoute = "api/v1/cheques/add";
        private string FindChequeRoute = "api/v1/cheques/find/{0}";
        private const string TransformChequeRoute = "api/v1/cheques/transform";

        public HttpClient HttpClient
        {
            get
            {
                return httpClientProvider.GetHttpClient();
            }
        }

        public ChequeController(IHttpClientProvider httpClientProvider)
        {
            this.httpClientProvider = httpClientProvider;
        }


        public ActionResult AddCheque(ChequeRequest request)
        {
            if (!ModelState.IsValid)
                return View("Add");

            HttpResponseMessage message = HttpClient.PostAsJsonAsync(AddChequeRoute, request).GetAwaiter().GetResult();
            if (message.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpError error = message.Content.ReadAsAsync<HttpError>().GetAwaiter().GetResult();
                return new HttpStatusCodeResult(message.StatusCode, error.Message);
            }

        }

        public bool AddSampleCheque(ChequeRequest request)
        {

            HttpResponseMessage message = HttpClient.PostAsJsonAsync(AddChequeRoute, request).GetAwaiter().GetResult();
            return message.IsSuccessStatusCode;
        }

        public ActionResult Add()
        {
            return View();
        }

        

        public ActionResult Transform(int id)
        {
            HttpResponseMessage transformedChequeMessage = new HttpResponseMessage();


            HttpResponseMessage chequeMessage = HttpClient.GetAsync(string.Format(FindChequeRoute, id)).GetAwaiter().GetResult();

            if (chequeMessage.IsSuccessStatusCode)
            {
                ChequeResponse originalCheque = chequeMessage.Content.ReadAsAsync<ChequeResponse>().GetAwaiter().GetResult();
                transformedChequeMessage = HttpClient.PutAsJsonAsync(TransformChequeRoute, originalCheque).GetAwaiter().GetResult();

                ChequeResponse updatedCheque = transformedChequeMessage.Content.ReadAsAsync<ChequeResponse>().GetAwaiter().GetResult();
                return View("Transform", updatedCheque);

            }
            else
            {
                HttpError error = chequeMessage.Content.ReadAsAsync<HttpError>().GetAwaiter().GetResult();
                return new HttpStatusCodeResult(transformedChequeMessage.StatusCode, error.Message);
            }

        }


    }
}
