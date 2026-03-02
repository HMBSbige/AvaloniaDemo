namespace AvaloniaDemo.Views.DesignViewModels;

public class DesignNugetDetailsViewModel : NugetDetailsViewModel
{
	public DesignNugetDetailsViewModel()
	{
		if (!Design.IsDesignMode)
		{
			throw new InvalidOperationException();
		}

		TransientCachedServiceProvider = AppLocator.Current.GetService<ITransientCachedServiceProvider>()!;

		Title = nameof(Title);
		Description = nameof(Description);
		IconUrl = DefaultIconUri;
		ProjectUrl = new Uri(@"https://github.com/HMBSbige");
	}
}
