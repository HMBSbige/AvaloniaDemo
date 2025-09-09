global using AvaloniaDemo.Domain.Shared;
global using JetBrains.Annotations;
global using Volo.Abp.Domain;
global using Volo.Abp.Modularity;

namespace AvaloniaDemo.Domain;

[UsedImplicitly]
[DependsOn(
	typeof(AbpDddDomainModule),
	typeof(AvaloniaDemoDomainSharedModule)
)]
public class AvaloniaDemoDomainModule : AbpModule;
