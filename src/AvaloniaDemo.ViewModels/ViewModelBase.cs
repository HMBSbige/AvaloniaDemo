namespace AvaloniaDemo.ViewModels;

public abstract class ViewModelBase : ReactiveObject
{
	public required ITransientCachedServiceProvider TransientCachedServiceProvider { get; [UsedImplicitly] init; }

	protected IServiceProvider ServiceProvider => TransientCachedServiceProvider.GetRequiredService<IServiceProvider>();
}
