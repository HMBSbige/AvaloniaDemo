namespace AvaloniaDemo;

public class App : Avalonia.Application
{
	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		IServiceProvider serviceProvider = Locator.Current.GetService<IServiceProvider>()!;

		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			desktop.Exit += (sender, e) => serviceProvider.GetRequiredService<IAbpApplication>().Shutdown();
			desktop.MainWindow = serviceProvider.GetRequiredService<MainWindow>();
		}
		else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
		{
			singleViewPlatform.MainView = serviceProvider.GetRequiredService<MainView>();
		}

		base.OnFrameworkInitializationCompleted();
	}
}
