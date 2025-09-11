global using Avalonia;
global using Avalonia.Controls;
global using Avalonia.Controls.ApplicationLifetimes;
global using Avalonia.Markup.Xaml;
global using Avalonia.ReactiveUI;
global using AvaloniaDemo.ViewModels;
global using AvaloniaDemo.Views;
global using JetBrains.Annotations;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using ReactiveUI;
global using Splat;
global using Splat.Microsoft.Extensions.DependencyInjection;
global using Volo.Abp;
global using Volo.Abp.Autofac;
global using Volo.Abp.DependencyInjection;
global using Volo.Abp.Modularity;

namespace AvaloniaDemo;

[DependsOn(
	typeof(AbpAutofacModule),
	typeof(AvaloniaDemoViewModelsModule)
)]
[UsedImplicitly]
public class AvaloniaDemoModule : AbpModule
{
	public override void PreConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.UseMicrosoftDependencyResolver();
	}

	public override void PostConfigureServices(ServiceConfigurationContext context)
	{
		#region AppBuilderExtensions.UseReactiveUI https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.ReactiveUI/AppBuilderExtensions.cs

		PlatformRegistrationManager.SetRegistrationNamespaces(RegistrationNamespace.Avalonia);
		RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;

		context.Services.AddSingleton<IActivationForViewFetcher, AvaloniaActivationForViewFetcher>();
		context.Services.AddSingleton<IPropertyBindingHook, AutoDataTemplateBindingHook>();

		#endregion
	}
}
