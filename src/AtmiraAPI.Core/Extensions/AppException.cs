namespace AtmiraAPI.Core.Extensions;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

// custom exception class for throwing application specific exceptions (e.g. for validation) 
// that can be caught and handled within the application
[ExcludeFromCodeCoverage]
public class AppException : Exception
{
  public AppException() : base() { }

  public AppException(string message) : base(message) { }

  public AppException(string message, params object[] args)
      : base(String.Format(CultureInfo.CurrentCulture, message, args))
  {
  }
}
