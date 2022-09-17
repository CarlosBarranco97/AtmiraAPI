using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmiraAPI.Core.Interfaces.Settings;
public interface INasaAPISettings
{
  public string BaseUrl { get; set; }
  public string ApiKey { get; set; }
}
