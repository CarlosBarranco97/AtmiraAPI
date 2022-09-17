using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace AtmiraAPI.SharedKernel;

[ExcludeFromCodeCoverage]
public class ModelOrError<T>
{
  public T? Model { get; set; }
  public string Error { get; set; }

  public IActionResult GetResult()
  {
    return string.IsNullOrEmpty(Error) ? new OkObjectResult(Model) : new BadRequestObjectResult(Error);
  }
}
