using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using deepend.api;
using deepend.api.Controllers;
using deepend.business.Common;
using deepend.business.Component;
using deepend.business.Interface;
using deepend.common;
using deepend.data;
using deepend.entity.Request;
using deepend.entity.Response;
using NSubstitute;
using NUnit.Framework;

namespace deepend.test
{
    [TestFixture]
    public class ChequeComponentTest
    {



        protected IConfigProvider configProvider;
        private string DatabaseConnectionString;
        private IChequeComponent chequeComponent;

        private IChequeData chequeData;
        private ChequeController chequeController;
        private const string allChequesRoute = "api/v1/cheques/all";
        private const string addChequeRoute = "api/v1/cheques/add";
        private  string findChequeRoute = "api/v1/cheques/find/{0}";
        private string transformChequeRoute = "api/v1/cheques/transform";

        protected HttpClient client;


        [OneTimeSetUp]
        public void TestInitialize()
        {

            configProvider = Substitute.For<IConfigProvider>();
            DatabaseConnectionString = GetIntegrationTestConnectionString();
            configProvider.GetConnectionString().Returns(DatabaseConnectionString);
            chequeComponent = new ChequeComponent();
            chequeData = new ChequeData(configProvider);
            chequeController = new ChequeController(chequeComponent, chequeData);
            client = new HttpClient()
            {
                BaseAddress = GetRequestUrl()
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }



        /// <summary>
        /// Given Provide Invalid Name and Amount 
        /// Cheque Is not Added
        /// Http Error is Returned
        /// </summary>
        [Test]

        public void AddChequeWithInvalidAmount()
        {

            //Arrange 
            ChequeRequest request = new ChequeRequest
            {

                PersonName = "12345",
                ChequeAmount = 0.0m
            };

            //Act 
            HttpResponseMessage response = client.PostAsJsonAsync(addChequeRoute, request).GetAwaiter().GetResult();


            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }


        /// <summary>
        /// Given Provided Invalid Name  
        /// Cheque Is not Added
        /// Exception Thrown and Error is Return 
        /// </summary>
        [Test]

        public void AddChequeWithInvalidName()
        {

            //Arrange 
            ChequeRequest request = new ChequeRequest
            {

                PersonName = "Jame123",
                ChequeAmount = 12.0m
            };

            //Act &Assert
            HttpResponseMessage response = client.PostAsJsonAsync(addChequeRoute, request).GetAwaiter().GetResult();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

        }


        /// <summary>
        /// Given Provided Valid Name  
        /// Cheque Is  Added
        ///Status Code is success and cheque saved to Database
        /// </summary>
        [Test]

        public void AddCheque()
        {

            //Arrange 
            ChequeRequest request = new ChequeRequest
            {

                PersonName = "Jame Brown",
                ChequeAmount = 1234.5m
            };

            //Act 

            var resultBefore = client.GetAsync(allChequesRoute).GetAwaiter().GetResult().Content.ReadAsAsync<IEnumerable<ChequeResponse>>().GetAwaiter().GetResult();
            var newChequeMessage = client.PostAsJsonAsync(addChequeRoute, request).GetAwaiter().GetResult();
            var id = newChequeMessage.Content.ReadAsAsync<int>().GetAwaiter().GetResult();
            var resultAfter = client.GetAsync(allChequesRoute).GetAwaiter().GetResult().Content.ReadAsAsync<IEnumerable<ChequeResponse>>().GetAwaiter().GetResult();

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, newChequeMessage.StatusCode);
            Assert.Greater(resultAfter.Count(), resultBefore.Count());
            Assert.Greater(id, resultBefore.Count());



        }


        /// <summary>
        /// Given Provided Valid Input  
        /// Cheque is  Added and retrieved 
        ///Status Code is OK and cheque saved to Database
        /// </summary>
        [Test]

        public void FindCheque()
        {

            //Arrange 
            ChequeRequest request = new ChequeRequest
            {

                PersonName = "New Person",
                ChequeAmount = 1234.5m
            };

            //Act 
           
            var newChequeMessage = client.PostAsJsonAsync(addChequeRoute, request).GetAwaiter().GetResult();
            var id = newChequeMessage.Content.ReadAsAsync<int>().GetAwaiter().GetResult();
            HttpResponseMessage response = client.GetAsync(string.Format(findChequeRoute, id)).GetAwaiter().GetResult();
            ChequeResponse cheque = response.Content.ReadAsAsync<ChequeResponse>().GetAwaiter().GetResult();
            
            //Assert
            Assert.AreEqual(HttpStatusCode.Created, newChequeMessage.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(cheque);
            Assert.AreEqual(cheque.PersonName, request.PersonName.ToUpper());

        }


        /// <summary>
        /// Given Provided Valid Input  
        /// Cheque is  Added and retrieved 
        ///Status Code is OK and cheque saved to Database
        /// </summary>
        [Test]

        public void TransformCheque()
        {

            //Arrange 
            ChequeRequest request = new ChequeRequest
            {

                PersonName = "Berry ",
                ChequeAmount = 1234.5m
            };

            string requestAmountInLetter = " one thousand and two hundred and thirty-four dollars and fifty cents ";

            //Act 

            var newChequeMessage = client.PostAsJsonAsync(addChequeRoute, request).GetAwaiter().GetResult();
            var chequeId = newChequeMessage.Content.ReadAsAsync<int>().GetAwaiter().GetResult();
            HttpResponseMessage response = client.GetAsync(string.Format(findChequeRoute, chequeId)).GetAwaiter().GetResult();
            ChequeResponse retrivedCheque = response.Content.ReadAsAsync<ChequeResponse>().GetAwaiter().GetResult();
            HttpResponseMessage updatedChequesResponse = client.PutAsJsonAsync(transformChequeRoute, retrivedCheque).GetAwaiter().GetResult();
            ChequeResponse updatedCheque = updatedChequesResponse.Content.ReadAsAsync<ChequeResponse>().GetAwaiter().GetResult();
            
            //Assert
            Assert.AreEqual(HttpStatusCode.Created, newChequeMessage.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            Assert.IsNotNull(retrivedCheque);
            Assert.AreEqual(retrivedCheque.PersonName, request.PersonName.ToUpper());

            Assert.AreEqual(HttpStatusCode.OK, updatedChequesResponse.StatusCode);
            Assert.IsNotNull(updatedCheque.ChequeAmountInLetter);
            Assert.AreEqual(requestAmountInLetter.Trim(), updatedCheque.ChequeAmountInLetter.Trim());

        }




        private string GetIntegrationTestConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["deependdb"].ConnectionString;
        }

        private Uri GetRequestUrl()
        {
            return new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
        }
    }
}
