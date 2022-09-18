using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using Module = Autofac.Module;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using AtmiraAPI.Core.Interfaces.Settings;
using AtmiraAPI.Infrastructure.Clients;
using AtmiraAPI.Core.Interfaces.Services;
using AtmiraAPI.Core.Constant;
using AtmiraAPI.Infrastructure.Settings;

namespace AtmiraAPI.Infrastructure;

[ExcludeFromCodeCoverage]
public class DefaultInfrastructureModule : Module
{
  private readonly ConfigurationManager _configuration;
  private readonly List<Assembly> _assemblies = new List<Assembly>();

  public DefaultInfrastructureModule(ConfigurationManager configuration,Assembly? callingAssembly = null)
  {
    _configuration = configuration;
    var infrastructureAssembly = Assembly.GetAssembly(typeof(StartupSetup));

    if (infrastructureAssembly != null)
    {
      _assemblies.Add(infrastructureAssembly);
    }
    if (callingAssembly != null)
    {
      _assemblies.Add(callingAssembly);
    }
  }

  protected override void Load(ContainerBuilder builder)
  {
    RegisterCommonDependencies(builder);
  }

  private void RegisterCommonDependencies(ContainerBuilder builder)
  {

    var nasaAPISettings = _configuration.BindSettings<NasaAPISettings>(SettingsConstants.NasaAPISettings);

    builder.RegisterInstance(nasaAPISettings)
      .As<INasaAPISettings>()
      .SingleInstance();

    builder.RegisterType<NasaClient>()
      .As<INasaClient>()
      .SingleInstance();

    builder
        .RegisterType<Mediator>()
        .As<IMediator>()
        .InstancePerLifetimeScope();

    builder.Register<ServiceFactory>(context =>
    {
      var c = context.Resolve<IComponentContext>();
      return t => c.Resolve(t);
    });

    var mediatrOpenTypes = new[]
    {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

    foreach (var mediatrOpenType in mediatrOpenTypes)
    {
      builder
      .RegisterAssemblyTypes(_assemblies.ToArray())
      .AsClosedTypesOf(mediatrOpenType)
      .AsImplementedInterfaces();
    }
  }

}
