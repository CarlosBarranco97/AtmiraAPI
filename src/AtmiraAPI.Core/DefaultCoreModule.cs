using System.Diagnostics.CodeAnalysis;
using AtmiraAPI.Core.Interfaces.Services;
using AtmiraAPI.Core.Services;
using Autofac;

namespace AtmiraAPI.Core;

[ExcludeFromCodeCoverage]
public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<AsteroidService>()
  .As<IAsteroidService>()
  .InstancePerLifetimeScope();

  }
}
