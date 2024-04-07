using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using System.Net;

namespace PassIn.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
  private readonly ILogger<ExceptionFilter> _logger;

  public ExceptionFilter(ILogger<ExceptionFilter> logger)
  {
    _logger = logger;
  }

  public void OnException(ExceptionContext context)
  {
    var result = context.Exception is PassInException;
    
    if (result)
    {
      HandleProjectException(context);
    }
    else
    {
      ThrowUnkonError(context);
    }
  }

  private void  HandleProjectException(ExceptionContext context)
  {
    if (context.Exception is NotFoundException)
    {
      context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
      context.Result = new NotFoundObjectResult(new ResponseErrorJson(context.Exception.Message));
    }

    if (context.Exception is ErrorValidationException)
    {
      context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
      context.Result = new BadRequestObjectResult(new ResponseErrorJson(context.Exception.Message));
    }

    if (context.Exception is ConflictException)
    {
      context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
      context.Result = new ConflictObjectResult(new ResponseErrorJson(context.Exception.Message));
    }
    _logger.LogError(context.Exception.ToString());
  }

  private void ThrowUnkonError(ExceptionContext context)
  {
    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    context.Result = new ObjectResult(new ResponseErrorJson("Erro desconhjecido!"));
    _logger.LogError(context.Exception.ToString());
  }
}
