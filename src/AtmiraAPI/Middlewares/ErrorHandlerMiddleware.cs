namespace AtmiraAPI.Middlewares;

using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using Serilog;
using AtmiraAPI.Core.Extensions;

[ExcludeFromCodeCoverage]
public class ErrorHandlerMiddleware
{
  private readonly RequestDelegate _next;

  public ErrorHandlerMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception error)
    {
      var response = context.Response;
      response.ContentType = "application/json";

      var id = Guid.NewGuid();
      Log.Error($"ErrorId = { id} --- {JsonSerializer.Serialize(new { message = error?.Message })}");
      var result = $"An error has happened, contact support providing the following ErrorId = { id}";
      switch (error)
      {
        case AppException e:
          // custom application error
          response.StatusCode = (int)HttpStatusCode.BadRequest;
          result = $"{JsonSerializer.Serialize(new { message = error?.Message })}  ---  ErrorId = { id}";
          break;
        case KeyNotFoundException e:
          // not found error
          response.StatusCode = (int)HttpStatusCode.NotFound;
          break;
        default:
          // unhandled error
          response.StatusCode = (int)HttpStatusCode.InternalServerError;
          break;
      }

      await response.WriteAsync(result);
    }
  }
}
