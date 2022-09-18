﻿using System;
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
    var response = await _nasaClient.GetAsteroidsData(planet);
    if (!string.IsNullOrEmpty(response.Error))
    {
      modelOrError.Error = response.Error;
      return modelOrError;
    }
    else if (response?.Model?.NearEarthObjects?.Values == null)
      return modelOrError;

    var listAsteroids = FilterAsteroids(planet, response?.Model?.NearEarthObjects?.Values?.ToList());

    if (listAsteroids.Count == 0)
      return modelOrError;

    modelOrError.Model = GenerateResponse(listAsteroids);

    return modelOrError;
  }

  private List<NearEarthObject> FilterAsteroids(string planet, List<NearEarthObject[]> model)
  {
    var listAsteroids = new List<NearEarthObject>();
    foreach (var nearEarthObject in model)
    {
      foreach (var asteroid in nearEarthObject)
      {
        if (asteroid != null &&
          asteroid.CloseApproachData?.FirstOrDefault()?.OrbitingBody.ToLower() == planet &&
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

  private static List<AsteroidResponse> GenerateResponse(List<NearEarthObject> listAsteroids)
  {
    var model = new List<AsteroidResponse>();
    foreach (var asteroid in listAsteroids)
    {
      model.Add(new AsteroidResponse()
      {
        Name = asteroid.Name,
        Date = asteroid.CloseApproachData.FirstOrDefault().CloseApproachDate,
        Diameter = (asteroid.EstimatedDiameter.Kilometers.MinDiamater + asteroid.EstimatedDiameter.Kilometers.MaxDiamater) / 2,
        Planet = asteroid.CloseApproachData.FirstOrDefault().OrbitingBody,
        Velocity = asteroid.CloseApproachData.FirstOrDefault().RelativeVelocity.KilometersPerHour
      });
    }

    return model;
  }

}