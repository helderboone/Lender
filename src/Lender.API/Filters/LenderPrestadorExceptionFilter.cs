using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Lender.API.Filters
{
    public class LenderPrestadorExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpResponse response = context.HttpContext.Response;

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            context.Result = new JsonResult($"Ocorreu um erro ao processar a requisição");
        }
    }
}
