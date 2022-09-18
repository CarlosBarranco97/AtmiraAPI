using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmiraAPI.Core.Models;
using AtmiraAPI.SharedKernel;

namespace AtmiraAPI.UnitTests.Fixtures;
public static class Fixtures
{
  public static readonly List<NearEarthObject> NearEarthObjects = CreateEarthObjects();
  public static readonly List<NearEarthObject[]> ArraysNearEarthObjects = CreateArraysNearEarthObjects();
  public static readonly ModelOrError<NasaResponse> GoodAPIResponse = CreateAPIResponse();
  public static readonly ModelOrError<NasaResponse> FailAPIResponse = CreateAPIFailResponse();

  private static ModelOrError<NasaResponse> CreateAPIFailResponse()
  {
    return new ModelOrError<NasaResponse>()
    {
      Error = "ErrorId",
      Model = null
    };
  }

  private static ModelOrError<NasaResponse> CreateAPIResponse()
  {
    var array = new NearEarthObject[]
        {
      new NearEarthObject()
      {
        CloseApproachData = new CloseApproachDateStruct[]
        {
          new CloseApproachDateStruct()
          {
            CloseApproachDate = DateTime.Now,
            RelativeVelocity = new RelativeVelocity()
            {
              KilometersPerHour = "533"
            },
            OrbitingBody = "Earth"
          }
        },
        IsPotentiallyHazardousAsteroid = true,
        EstimatedDiameter = new EstimatedDiameter
        {
          Kilometers = new Kilometers()
          {
            MaxDiamater = 5,
            MinDiamater = 2
          }
        },
        Name ="Asteroid1"
      },
      new NearEarthObject() {
        CloseApproachData = new CloseApproachDateStruct[]
        {
          new CloseApproachDateStruct()
          {
            CloseApproachDate = DateTime.Now,
            RelativeVelocity = new RelativeVelocity()
            {
              KilometersPerHour = "533"
            },
            OrbitingBody = "Earth"
          }
        },
        IsPotentiallyHazardousAsteroid = true,
        EstimatedDiameter = new EstimatedDiameter
        {
          Kilometers = new Kilometers()
          {
            MaxDiamater = 5,
            MinDiamater = 2
          }
        },
        Name ="Asteroid2"
      },
      new NearEarthObject() {
        CloseApproachData = new CloseApproachDateStruct[]
        {
          new CloseApproachDateStruct()
          {
            CloseApproachDate = DateTime.Now,
            RelativeVelocity = new RelativeVelocity()
            {
              KilometersPerHour = "533"
            },
            OrbitingBody = "Earth"
          }
        },
        IsPotentiallyHazardousAsteroid = true,
        EstimatedDiameter = new EstimatedDiameter
        {
          Kilometers = new Kilometers()
          {
            MaxDiamater = 5,
            MinDiamater = 2
          }
        },
        Name ="Asteroid3"
      },
      new NearEarthObject() {
        CloseApproachData = new CloseApproachDateStruct[]
        {
          new CloseApproachDateStruct()
          {
            CloseApproachDate = DateTime.Now,
            RelativeVelocity = new RelativeVelocity()
            {
              KilometersPerHour = "533"
            },
            OrbitingBody = "Mars"
          }
        },
        IsPotentiallyHazardousAsteroid = true,
        EstimatedDiameter = new EstimatedDiameter
        {
          Kilometers = new Kilometers()
          {
            MaxDiamater = 5,
            MinDiamater = 2
          }
        },
        Name ="Asteroid4"
      }
        };
    return new ModelOrError<NasaResponse>()
    {
      Model = new NasaResponse()
      {
        NearEarthObjects = new Dictionary<string, NearEarthObject[]>()
        {
          {"day 1", array }
        }
      }
    };
  }

  private static List<NearEarthObject[]> CreateArraysNearEarthObjects()
  {
    var array = new NearEarthObject[]
    {
      new NearEarthObject()
      {
        CloseApproachData = new CloseApproachDateStruct[]
        {
          new CloseApproachDateStruct()
          {
            CloseApproachDate = DateTime.Now,
            RelativeVelocity = new RelativeVelocity()
            {
              KilometersPerHour = "533"
            },
            OrbitingBody = "Earth"
          }
        },
        IsPotentiallyHazardousAsteroid = true,
        EstimatedDiameter = new EstimatedDiameter
        {
          Kilometers = new Kilometers()
          {
            MaxDiamater = 5,
            MinDiamater = 2
          }
        },
        Name ="Asteroid1"
      },
      new NearEarthObject() {
        CloseApproachData = new CloseApproachDateStruct[]
        {
          new CloseApproachDateStruct()
          {
            CloseApproachDate = DateTime.Now,
            RelativeVelocity = new RelativeVelocity()
            {
              KilometersPerHour = "533"
            },
            OrbitingBody = "Earth"
          }
        },
        IsPotentiallyHazardousAsteroid = true,
        EstimatedDiameter = new EstimatedDiameter
        {
          Kilometers = new Kilometers()
          {
            MaxDiamater = 5,
            MinDiamater = 2
          }
        },
        Name ="Asteroid2"
      },
      new NearEarthObject() {
        CloseApproachData = new CloseApproachDateStruct[]
        {
          new CloseApproachDateStruct()
          {
            CloseApproachDate = DateTime.Now,
            RelativeVelocity = new RelativeVelocity()
            {
              KilometersPerHour = "533"
            },
            OrbitingBody = "Earth"
          }
        },
        IsPotentiallyHazardousAsteroid = true,
        EstimatedDiameter = new EstimatedDiameter
        {
          Kilometers = new Kilometers()
          {
            MaxDiamater = 5,
            MinDiamater = 2
          }
        },
        Name ="Asteroid3"
      },
      new NearEarthObject() {
        CloseApproachData = new CloseApproachDateStruct[]
        {
          new CloseApproachDateStruct()
          {
            CloseApproachDate = DateTime.Now,
            RelativeVelocity = new RelativeVelocity()
            {
              KilometersPerHour = "533"
            },
            OrbitingBody = "Mars"
          }
        },
        IsPotentiallyHazardousAsteroid = true,
        EstimatedDiameter = new EstimatedDiameter
        {
          Kilometers = new Kilometers()
          {
            MaxDiamater = 5,
            MinDiamater = 2
          }
        },
        Name ="Asteroid4"
      }
    };
    return new List<NearEarthObject[]>
    {
      array
    };
  }
  private static List<NearEarthObject> CreateEarthObjects()
  {
    return new List<NearEarthObject>()
    {
      new NearEarthObject()
      {
        CloseApproachData = new CloseApproachDateStruct[]
        {
          new CloseApproachDateStruct()
          {
            CloseApproachDate = DateTime.Now,
            RelativeVelocity = new RelativeVelocity()
            {
              KilometersPerHour = "533"
            }
          }
        },
        IsPotentiallyHazardousAsteroid = true,
        EstimatedDiameter = new EstimatedDiameter
        {
          Kilometers = new Kilometers()
          {
            MaxDiamater = 5,
            MinDiamater = 2
          }
        },
        Name ="Asteroid1"
      },
      new NearEarthObject() {
        CloseApproachData = new CloseApproachDateStruct[]
        {
          new CloseApproachDateStruct()
          {
            CloseApproachDate = DateTime.Now,
            RelativeVelocity = new RelativeVelocity()
            {
              KilometersPerHour = "533"
            }
          }
        },
        IsPotentiallyHazardousAsteroid = true,
        EstimatedDiameter = new EstimatedDiameter
        {
          Kilometers = new Kilometers()
          {
            MaxDiamater = 5,
            MinDiamater = 2
          }
        },
        Name ="Asteroid2"
      },
      new NearEarthObject() {
        CloseApproachData = new CloseApproachDateStruct[]
        {
          new CloseApproachDateStruct()
          {
            CloseApproachDate = DateTime.Now,
            RelativeVelocity = new RelativeVelocity()
            {
              KilometersPerHour = "533"
            }
          }
        },
        IsPotentiallyHazardousAsteroid = true,
        EstimatedDiameter = new EstimatedDiameter
        {
          Kilometers = new Kilometers()
          {
            MaxDiamater = 5,
            MinDiamater = 2
          }
        },
        Name ="Asteroid3"
      }
    };
  }
}
