using AdoPet.Communication.Responses;
using AdoPet.Exception;
using AdoPet.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdoPet.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is AdoPetException adoPetException)
        {
            context.HttpContext.Response.StatusCode = (int)adoPetException.GetStatusCode();

            context.Result = new ObjectResult(new ResponseErrorJson(adoPetException.GetErrorsMessages()));
        }

        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
        }
    }
}
