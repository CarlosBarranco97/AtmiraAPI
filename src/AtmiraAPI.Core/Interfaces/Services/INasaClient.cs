using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmiraAPI.Core.Models;
using AtmiraAPI.SharedKernel;

namespace AtmiraAPI.Core.Interfaces.Services;
public interface INasaClient
{
  Task<ModelOrError<NasaResponse>> GetAsteroidsData();
}
