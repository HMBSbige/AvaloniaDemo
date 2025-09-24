using Avalonia;
using Splat.Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace AvaloniaDemo.Desktop;

internal static class Program
{
	/// <summary>
	/// Initialization code. Don't use any Avalonia, third-party APIs or any
	/// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	/// yet and stuff might break.
	/// </summary>
	[STAThread]
	public static int Main(string[] args)
	{
		return BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
	}

	/// <summary>
	/// Avalonia configuration, don't remove; also used by visual designer.
	/// </summary>
	private static AppBuilder BuildAvaloniaApp()
	{
		IAbpApplicationWithInternalServiceProvider application = AbpApplicationFactory.Create<AvaloniaDemoModule>(options => options.UseAutofac());
		application.Initialize();
		application.ServiceProvider.UseMicrosoftDependencyResolver();

		return AppBuilder.Configure<App>()
				.UsePlatformDetect()
				.LogToTrace()
				.With(new Win32PlatformOptions { RenderingMode = [Win32RenderingMode.AngleEgl, Win32RenderingMode.Vulkan, Win32RenderingMode.Wgl, Win32RenderingMode.Software] })
				.With(new X11PlatformOptions { RenderingMode = [X11RenderingMode.Vulkan, X11RenderingMode.Egl, X11RenderingMode.Glx, X11RenderingMode.Software] })
			;
	}
}
