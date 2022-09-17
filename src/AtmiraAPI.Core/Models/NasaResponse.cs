using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AtmiraAPI.Core.Models;

public partial class Kilometers
{
  [JsonProperty("estimated_diameter_min")]
  public double MinDiamater { get; set; }
  [JsonProperty("estimated_diameter_max")]
  public double MaxDiamater { get; set; }
}

public partial class RelativeVelocity
{

  [JsonProperty("kilometers_per_hour")]
  public string KilometersPerHour { get; set; }

}

public partial class EstimatedDiameter
{
  [JsonProperty("kilometers")]
  public Kilometers Kilometers { get; set; }
}
public partial class CloseApproachDateStruct
{
  [JsonProperty("close_approach_date")]
  public DateTimeOffset CloseApproachDate { get; set; }

  [JsonProperty("relative_velocity")]
  public RelativeVelocity RelativeVelocity { get; set; }

  [JsonProperty("orbiting_body")]
  public string OrbitingBody { get; set; }
}

public partial class NearEarthObject
{
  [JsonProperty("estimated_diameter")]
  public EstimatedDiameter EstimatedDiameter { get; set; }

  [JsonProperty("is_potentially_hazardous_asteroid")]
  public bool IsPotentiallyHazardousAsteroid { get; set; }

  [JsonProperty("close_approach_data")]
  public CloseApproachDateStruct[] CloseApproachData { get; set; }

  [JsonProperty("name")]
  public string Name { get; set; }

}
public class NasaResponse
{
  [JsonProperty("near_earth_objects")]
  public Dictionary<string, NearEarthObject[]> NearEarthObjects { get; set; }

}
