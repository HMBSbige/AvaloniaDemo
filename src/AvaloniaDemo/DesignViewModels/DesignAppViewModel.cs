namespace AvaloniaDemo.DesignViewModels;

public class DesignAppViewModel : AppViewModel
{
	public DesignAppViewModel()
	{
		if (!Design.IsDesignMode)
		{
			throw new InvalidOperationException();
		}

		TransientCachedServiceProvider = Locator.Current.GetService<ITransientCachedServiceProvider>()!;
	}
}
