using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmiraAPI.Core.Interfaces.Services;
using AtmiraAPI.Core.Interfaces.Settings;
using AtmiraAPI.Core.Models;
using AtmiraAPI.SharedKernel;
using Newtonsoft.Json;

namespace AtmiraAPI.Infrastructure.Clients;
public class NasaClient : INasaClient
{
  private readonly HttpClient _httpClient;
  private readonly INasaAPISettings _nasaAPISettings;

  public NasaClient(INasaAPISettings nasaAPISettings)
  {
    _nasaAPISettings = nasaAPISettings;
    _httpClient = new HttpClient()
    {
      BaseAddress = new Uri(_nasaAPISettings.BaseUrl)
    };
  }

  public async Task<ModelOrError<NasaResponse>> GetAsteroidsData(string planet)
  {
    var modelOrError = new ModelOrError<NasaResponse>();
    var today = DateTime.Now.ToString("yyyy-MM-dd");
    var nextDays = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");

    var response = await _httpClient.GetAsync($"/neo/rest/v1/feed?start_date={today}&end_date={nextDays}&api_key={_nasaAPISettings.ApiKey}");

    if (!response.IsSuccessStatusCode)
    {
      modelOrError.Error = "Getting Error from Nasa API: " + response.StatusCode.ToString();
    }
    else
    {
      modelOrError.Model = JsonConvert.DeserializeObject<NasaResponse>(await response.Content.ReadAsStringAsync());
    }
    return modelOrError;
  }
}
