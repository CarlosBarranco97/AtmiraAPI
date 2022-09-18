using System.ComponentModel.DataAnnotations;
using AtmiraAPI.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AtmiraAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AsteroidController : ControllerBase
{
  private readonly IAsteroidService _asteroidService;
  public AsteroidController(IAsteroidService asteroidService)
  {
    _asteroidService = asteroidService;
  }

  [HttpGet()]
  [ProducesResponseType(typeof(string), 200)]
  [ProducesResponseType(typeof(string), 400)]
  public async Task<IActionResult> GetAsteroidFromPlanet([FromQuery][Required] string planet)
  {
    var response = await _asteroidService.GetAsteroidFromPlanet(planet);
    return response.GetResult();
  }
}
