using deepend.business.Common;
using deepend.entity.Request;
using deepend.entity.Response;
using deepend.business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using deepend.data;

namespace deepend.api.Controllers
{
    [RoutePrefix("api/v1/cheques")]
    public class ChequeController : ApiController
    {
        private readonly IChequeComponent chequeComponent;
        private readonly IChequeData chequeData;



        public ChequeController(IChequeComponent chequeComponent, IChequeData chequeData)
        {
            this.chequeComponent = chequeComponent;
            this.chequeData = chequeData;
        }

        [Route("all")]
        [HttpGet]
        public HttpResponseMessage GetAllCheques()
        {
            try
            {
                HttpResponseMessage response;
                IEnumerable<ChequeResponse> allCheques = chequeData.GetAllCheque();
                if (allCheques != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, allCheques);
                    return response;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NoContent);
                    return response;
                }
                

            }
            catch (Exception ex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message, ex);
                return errorResponse;
            }
        }

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]ChequeRequest request)
        {
            try
            {
                ChequeResponse newCheque = chequeComponent.Create(request);
                int recordId=chequeData.AddCheque(newCheque);
                return Request.CreateResponse(HttpStatusCode.Created,recordId);
            }
            catch (ValidationException ex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message, ex);
                return errorResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message, ex);
                return errorResponse;
            }

        }

        [Route("transform")]
        [HttpPut]
        public HttpResponseMessage Update([FromBody] ChequeResponse response)
        {
            try
            {
                ChequeResponse updatedCheque = chequeComponent.Transform(response);
                return Request.CreateResponse(HttpStatusCode.OK, updatedCheque);
            }
            catch (Exception ex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message, ex);
                return errorResponse;
            }

        }

        [Route("find/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                ChequeResponse cheque = chequeData.FindCheque(id);
                if (cheque != null)
                    return Request.CreateResponse(HttpStatusCode.OK, cheque);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);

            }
            catch (Exception ex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message, ex);
                return errorResponse;
            }
        }


    }
}
