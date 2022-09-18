using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmiraAPI.Core.Interfaces.Settings;

namespace AtmiraAPI.Infrastructure.Settings;


public class NasaAPISettings : INasaAPISettings
{
  public string BaseUrl { get; set; } = String.Empty;
  public string ApiKey { get; set; } = String.Empty;
}
