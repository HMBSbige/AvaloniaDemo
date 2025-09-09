global using AvaloniaDemo.Domain.Shared;
global using JetBrains.Annotations;
global using Volo.Abp.Application;
global using Volo.Abp.Modularity;

namespace AvaloniaDemo.Application.Contracts;

[UsedImplicitly]
[DependsOn(
	typeof(AbpDddApplicationContractsModule),
	typeof(AvaloniaDemoDomainSharedModule)
)]
public class AvaloniaDemoApplicationContractsModule : AbpModule;
