using Avalonia.Media.Imaging;
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

	[Reactive]
	public partial Task<Bitmap?> Icon { get; set; }

	private readonly CompositeDisposable _disposable = new();

	[RequiresUnreferencedCode("WhenAnyValue may reference members that could be trimmed.")]
	public NugetDetailsViewModel()
	{
		if (Design.IsDesignMode)
		{
			Title = nameof(Title);
			Description = nameof(Description);
		}

		Icon = Task.FromResult<Bitmap?>(default);

		this.WhenAnyValue(x => x.IconUrl)
			.Select(url => LoadIcon(url))
			.Subscribe(task => Icon = task)
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

	private async Task<Bitmap?> LoadIcon(Uri? url, CancellationToken cancellationToken = default)
	{
		if (url is null)
		{
			return default;
		}

		try
		{
			byte[] data = await TransientCachedServiceProvider.GetRequiredService<NugetSearchAppService>().DownloadIconAsync(url, cancellationToken);

			await using Stream stream = new MemoryStream(data);

			return new Bitmap(stream);
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
