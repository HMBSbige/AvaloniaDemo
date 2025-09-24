global using Avalonia;
global using Avalonia.Controls;
global using Avalonia.Controls.ApplicationLifetimes;
global using Avalonia.Markup.Xaml;
global using Avalonia.ReactiveUI;
global using AvaloniaDemo.ViewModels;
global using AvaloniaDemo.Views;
global using JetBrains.Annotations;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using ReactiveUI;
global using Serilog;
global using Serilog.Core;
global using Serilog.Events;
global using Splat;
global using Splat.Microsoft.Extensions.DependencyInjection;
global using Splat.Serilog;
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

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ConfigureLogging(context);
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

	private static void ConfigureLogging(ServiceConfigurationContext context)
	{
#if DEBUG
		Serilog.Debugging.SelfLog.Enable(msg =>
		{
			System.Diagnostics.Debug.Print(msg);
			System.Diagnostics.Debugger.Break();
		});
#endif

		Logger logger = new LoggerConfiguration()
#if DEBUG
			.MinimumLevel.Debug()
#else
			.MinimumLevel.Information()
#endif
			.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
			.Enrich.FromLogContext()
#if DEBUG
			.WriteTo.Async(c => c.Debug(outputTemplate: @"[{Timestamp:O}] [{Level}] {Message:lj}{NewLine}{Exception}"))
#endif
			.CreateLogger();

		Locator.CurrentMutable.UseSerilogFullLogger(logger);

		context.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger, true));
	}

	public override void OnApplicationShutdown(ApplicationShutdownContext context)
	{
		context.ServiceProvider.GetService<ILoggerProvider>()?.Dispose();
	}
}
