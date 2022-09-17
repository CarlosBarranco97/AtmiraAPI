using System.Diagnostics.CodeAnalysis;

namespace AtmiraAPI.Core.Models;
public class SettingsInfo
{
  public readonly string SectionName;
  public readonly string EnvironmentPrefix;

  [ExcludeFromCodeCoverage]
  public SettingsInfo(string sectionName, string environmentPrefix = null)
  {
    SectionName = sectionName;
    EnvironmentPrefix = environmentPrefix;
  }
}
