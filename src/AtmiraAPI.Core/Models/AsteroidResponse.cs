using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmiraAPI.Core.Models;
public class AsteroidResponse
{
  public string Name { get; set; }
  public double Diameter { get; set; }
  public string Velocity { get; set; }
  public DateTimeOffset Date { get; set; }
  public string Planet { get; set; }
}
