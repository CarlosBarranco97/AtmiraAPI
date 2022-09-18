namespace AtmiraAPI.UnitTests.Services
{
  using System;
  using NUnit.Framework;
  using FluentAssertions;
  using Moq;
  using AutoFixture;
  using AutoFixture.AutoMoq;
  using AtmiraAPI.SharedKernel;
  using AtmiraAPI.Core.Models;
  using System.Collections.Generic;
  using AtmiraAPI.Core.Interfaces.Services;
  using System.Threading.Tasks;
  using AtmiraAPI.Core.Services;

  [TestFixture]
  public class AsteroidServiceTests
  {
    private AsteroidService _testClass;
    private Mock<INasaClient> _nasaClient;

    [SetUp]
    public void SetUp()
    {
      var fixture = new Fixture().Customize(new AutoMoqCustomization());
      _nasaClient = fixture.Freeze<Mock<INasaClient>>();
      _testClass = fixture.Create<AsteroidService>();
    }

    [TestCase("Earth",false, true)]
    [TestCase("Earth", true, false)]
    [TestCase(null, true, false)]
    [TestCase("", false, false)]
    public async Task CanCallGetAsteroidFromPlanet(string planet, bool errorFromAPI, bool expectedResult)
    {
      // Arrange
      var fixture = new Fixture().Customize(new AutoMoqCustomization());
      if(errorFromAPI)
        _nasaClient.Setup(mock => mock.GetAsteroidsData()).ReturnsAsync(Fixtures.Fixtures.FailAPIResponse);
      else
        _nasaClient.Setup(mock => mock.GetAsteroidsData()).ReturnsAsync(Fixtures.Fixtures.GoodAPIResponse);

      // Act
      var result = await _testClass.GetAsteroidFromPlanet(planet);

      // Assert


      if (expectedResult)
      {
        _nasaClient.Verify(mock => mock.GetAsteroidsData());
        Assert.That(result.Error, Is.EqualTo(null));
      }
      else
        Assert.That(result.Error, Is.Not.EqualTo(string.Empty));
    }

    [TestCase("Earth", 3)]
    [TestCase("Mars", 1)]
    [TestCase("Venus", 0)]
    public void CanCallFilterAsteroids(string _planet, int finalCount)
    {
      // Arrange
      var fixture = new Fixture().Customize(new AutoMoqCustomization());
      var model = Fixtures.Fixtures.ArraysNearEarthObjects;

      // Act
      var result = AsteroidService.FilterAsteroids(_planet, model);

      // Assert
      Assert.That(result.Count, Is.EqualTo(finalCount));
    }

    [Test]
    public void CanCallGenerateResponse()
    {
      // Arrange
      var listAsteroids = Fixtures.Fixtures.NearEarthObjects;

      // Act
      var result = AsteroidService.GenerateResponse(listAsteroids);

      // Assert
      result.Count.Should().Be(listAsteroids.Count);
      foreach(var item in result)
      {
        var asteroid = listAsteroids[result.IndexOf(item)];
        var diameter = (asteroid.EstimatedDiameter.Kilometers.MinDiamater + asteroid.EstimatedDiameter.Kilometers.MaxDiamater)/2;
        Assert.That(item.Name, Is.EqualTo(asteroid.Name));
        Assert.That(item.Velocity, Is.EqualTo(asteroid.CloseApproachData?.FirstOrDefault()?.RelativeVelocity.KilometersPerHour));
        Assert.That(item.Planet, Is.EqualTo(asteroid.CloseApproachData?.FirstOrDefault()?.OrbitingBody));
        Assert.That(item.Date, Is.EqualTo(asteroid.CloseApproachData?.FirstOrDefault()?.CloseApproachDate));
        Assert.That(item.Diameter, Is.EqualTo(diameter));
      }
      
    }

    [Test]
    public void CannotCallGenerateResponseWithNullListAsteroids()
    {
      var fixture = new Fixture().Customize(new AutoMoqCustomization());
      var listAsteroids = fixture.Create<List<NearEarthObject>>();

      // Act
      var result = AsteroidService.GenerateResponse(listAsteroids);
      if (listAsteroids == null || listAsteroids.Count == 0)
        Assert.That(result.Count, Is.EqualTo(0));
      else
        Assert.That(result.Count, Is.Not.EqualTo(0));
    }
  }
}
