namespace AvaloniaDemo.DesignViewModels;

public class DesignNugetDetailsViewModel : NugetDetailsViewModel
{
	public DesignNugetDetailsViewModel()
	{
		if (!Design.IsDesignMode)
		{
			throw new InvalidOperationException();
		}

		TransientCachedServiceProvider = Locator.Current.GetService<ITransientCachedServiceProvider>()!;

		Title = nameof(Title);
		Description = nameof(Description);
		IconUrl = DefaultIconUri;
		ProjectUrl = new Uri(@"https://github.com/HMBSbige");
	}
}
