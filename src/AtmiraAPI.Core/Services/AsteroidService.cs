using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmiraAPI.Core.Interfaces.Services;
using AtmiraAPI.Core.Models;
using AtmiraAPI.SharedKernel;
using Newtonsoft.Json;

namespace AtmiraAPI.Core.Services;
public class AsteroidService : IAsteroidService
{
  private readonly INasaClient _nasaClient;
  public AsteroidService(INasaClient nasaClient)
  {
    _nasaClient = nasaClient;
  }
  public async Task<ModelOrError<List<AsteroidResponse>>> GetAsteroidFromPlanet(string planet)
  {
    var modelOrError = new ModelOrError<List<AsteroidResponse>>();
    
    if (string.IsNullOrEmpty(planet))
    {
      modelOrError.Error = "Planet cannot be null";
      return modelOrError;
    }

    var response = await _nasaClient.GetAsteroidsData();

    if (!string.IsNullOrEmpty(response.Error))
    {
      modelOrError.Error = response.Error;
      return modelOrError;
    }
    else if (response?.Model?.NearEarthObjects?.Values == null || response?.Model?.NearEarthObjects?.Values.Count == 0)
    {
      modelOrError.Model = new List<AsteroidResponse>();
      return modelOrError;
    }
      

    var listAsteroids = FilterAsteroids(planet, response?.Model?.NearEarthObjects?.Values?.ToList());

    if (listAsteroids.Count == 0)
    {
      modelOrError.Model = new List<AsteroidResponse>();
      return modelOrError;
    }

    modelOrError.Model = GenerateResponse(listAsteroids);

    return modelOrError;
  }

  public static List<NearEarthObject> FilterAsteroids(string planet, List<NearEarthObject[]> model)
  {
    var listAsteroids = new List<NearEarthObject>();
    foreach (var nearEarthObject in model)
    {
      foreach (var asteroid in nearEarthObject)
      {
        if (asteroid != null &&
          asteroid.CloseApproachData?.FirstOrDefault()?.OrbitingBody.ToLower() == planet.ToLower() &&
          asteroid.IsPotentiallyHazardousAsteroid)
        {
          listAsteroids.Add(asteroid);
        }
      }
    }
    return listAsteroids.Count > 0 ?
     listAsteroids.OrderByDescending
      (x => (x.EstimatedDiameter.Kilometers.MinDiamater + x.EstimatedDiameter.Kilometers.MaxDiamater) / 2)
      .Take(3).ToList() : listAsteroids;
  }

  public static List<AsteroidResponse> GenerateResponse(List<NearEarthObject> listAsteroids)
  {
    return listAsteroids.Select(x => new AsteroidResponse()
    {
      Name = x.Name,
      Date = x.CloseApproachData.FirstOrDefault()?.CloseApproachDate ?? new DateTimeOffset(),
      Diameter = (x.EstimatedDiameter.Kilometers.MinDiamater + x.EstimatedDiameter.Kilometers.MaxDiamater) / 2,
      Planet = x.CloseApproachData.FirstOrDefault()?.OrbitingBody,
      Velocity = x.CloseApproachData.FirstOrDefault()?.RelativeVelocity?.KilometersPerHour
    }).ToList(); 

  }

}
