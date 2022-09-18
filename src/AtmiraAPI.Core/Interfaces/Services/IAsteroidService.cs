using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmiraAPI.Core.Models;
using AtmiraAPI.SharedKernel;

namespace AtmiraAPI.Core.Interfaces.Services;
public interface IAsteroidService
{
  Task<ModelOrError<List<AsteroidResponse>>> GetAsteroidFromPlanet(string planet);
}
