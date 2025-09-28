global using AvaloniaDemo.Domain.Shared.Localization;
global using JetBrains.Annotations;
global using Volo.Abp.Localization;
global using Volo.Abp.Modularity;
global using Volo.Abp.VirtualFileSystem;

namespace AvaloniaDemo.Domain.Shared;

[UsedImplicitly]
[DependsOn(typeof(AbpLocalizationModule))]
public class AvaloniaDemoDomainSharedModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpVirtualFileSystemOptions>(options => options.FileSets.AddEmbedded<AvaloniaDemoDomainSharedModule>(typeof(AvaloniaDemoDomainSharedModule).Namespace));

		Configure<AbpLocalizationOptions>(options =>
		{
			options.DefaultResourceType = typeof(AvaloniaDemoResource);

			options.Resources
				.Add<AvaloniaDemoResource>("en")
				.AddVirtualJson("/Localization/AvaloniaDemo");
		});
	}
}
