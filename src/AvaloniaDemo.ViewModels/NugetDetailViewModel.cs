using System.Diagnostics;

namespace AvaloniaDemo.ViewModels;

public partial class NugetDetailsViewModel : ViewModelBase, ITransientDependency, IDisposable
{
	[Reactive]
	public partial string? Title { get; set; }

	[Reactive]
	public partial string? Description { get; set; }

	[Reactive]
	public partial Uri? IconUrl { get; set; }

	[Reactive]
	public partial Uri? ProjectUrl { get; set; }

	[ObservableAsProperty]
	public partial byte[]? Icon { get; }

	public static readonly Uri DefaultIconUri = new(@"https://raw.githubusercontent.com/NuGet/Media/main/Images/MainLogo/64x64/nuget_64.png");

	private readonly CompositeDisposable _disposable = new();

	[RequiresUnreferencedCode("WhenAnyValue may reference members that could be trimmed.")]
	public NugetDetailsViewModel()
	{
		this.WhenAnyValue(x => x.IconUrl)
			.Select(url => Observable.FromAsync(cancellationToken => LoadIconAsync(url, cancellationToken)))
			.Switch()
			.ToProperty(this, x => x.Icon, out _iconHelper)
			.DisposeWith(_disposable);

		OpenPageCommand.DisposeWith(_disposable);
	}

	[ReactiveCommand(OutputScheduler = nameof(RxApp.TaskpoolScheduler))]
	private void OpenPage()
	{
		if (ProjectUrl is null || ProjectUrl.Scheme != Uri.UriSchemeHttps)
		{
			return;
		}

		using Process? process = Process.Start(new ProcessStartInfo(ProjectUrl.ToString()) { UseShellExecute = true });
	}

	private async Task<byte[]?> LoadIconAsync(Uri? url, CancellationToken cancellationToken = default)
	{
		if (url is null)
		{
			return default;
		}

		try
		{
			return await TransientCachedServiceProvider.GetRequiredService<NugetSearchAppService>().DownloadIconAsync(url, cancellationToken);
		}
		catch
		{
			return default;
		}
	}

	public void Dispose()
	{
		_disposable.Dispose();
		GC.SuppressFinalize(this);
	}
}
