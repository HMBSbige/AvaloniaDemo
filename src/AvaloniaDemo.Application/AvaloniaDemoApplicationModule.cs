global using AvaloniaDemo.Application.Contracts;
global using AvaloniaDemo.Domain;
global using JetBrains.Annotations;
global using Microsoft.Extensions.DependencyInjection;
global using NuGet.Protocol;
global using NuGet.Protocol.Core.Types;
global using Volo.Abp.Application;
global using Volo.Abp.Modularity;

namespace AvaloniaDemo.Application;

[UsedImplicitly]
[DependsOn(
	typeof(AbpDddApplicationModule),
	typeof(AvaloniaDemoApplicationContractsModule),
	typeof(AvaloniaDemoDomainModule)
)]
public class AvaloniaDemoApplicationModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddSingleton(Repository.Factory.GetCoreV3(@"https://api.nuget.org/v3/index.json"));
		context.Services.AddHttpClient();
	}
}
