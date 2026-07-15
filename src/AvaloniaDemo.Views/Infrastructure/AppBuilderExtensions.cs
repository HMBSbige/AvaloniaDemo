using Autofac.Extensions.DependencyInjection;
using Avalonia;
using ReactiveUI.Avalonia.Splat;

namespace AvaloniaDemo.Views.Infrastructure;

public static class AppBuilderExtensions
{
	public static AppBuilder UseAvaloniaDemoApp(this AppBuilder builder)
	{
		return builder.UseReactiveUIWithAutofac
		(
			containerBuilder =>
			{
				ServiceCollection services = new();

				services.AddApplication<AvaloniaDemoViewsModule>();

				containerBuilder.Populate(services);
			},
			withResolver: resolver =>
			{
				IServiceProvider serviceProvider = resolver.GetService<IServiceProvider>()
					?? throw new InvalidOperationException("IServiceProvider is not registered.");

				IAbpApplicationWithExternalServiceProvider application = resolver.GetService<IAbpApplicationWithExternalServiceProvider>()
					?? throw new InvalidOperationException("ABP application is not registered.");

				application.Initialize(serviceProvider);
			}
		);
	}
}
