namespace AvaloniaDemo.ViewModels;

public partial class AppViewModel : ViewModelBase, ISingletonDependency
{
	[Reactive]
	public partial string? SearchTerm { get; set; }

	private readonly SourceList<NugetDetailsViewModel> _packages = new();

	[BindableDerivedList]
	private readonly ReadOnlyObservableCollection<NugetDetailsViewModel> _searchResults;

	public AppViewModel()
	{
		_packages.Connect()
			.Bind(out _searchResults)
			.DisposeMany()
			.Subscribe();

		this.WhenAnyValue(x => x.SearchTerm)
			.Throttle(TimeSpan.FromMilliseconds(500))
			.Select(term => term?.Trim())
			.DistinctUntilChanged()
			.Select(s => Observable.FromAsync(cancellationToken => SearchNuGetPackagesAsync(s, cancellationToken))
				.Catch<IEnumerable<NugetDetailsViewModel>, Exception>(ex =>
				{
					this.Log().Error(ex, @"Error!");
					return Observable.Return(Array.Empty<NugetDetailsViewModel>());
				})
			)
			.Switch()
			.Subscribe(results =>
			{
				_packages.Edit(inner =>
				{
					inner.Clear();
					inner.AddRange(results);
				});
			});
	}

	private async Task<IEnumerable<NugetDetailsViewModel>> SearchNuGetPackagesAsync(string? term, CancellationToken cancellationToken)
	{
		NugetSearchAppService service = TransientCachedServiceProvider.GetRequiredService<NugetSearchAppService>();

		IEnumerable<NugetPackageInfoDto> result = await service.SearchAsync(
			new NugetSearchRequestDto
			{
				SearchTerm = term,
				SkipCount = 0,
				MaxResultCount = 10
			},
			cancellationToken);

		return result.Select(x =>
		{
			NugetDetailsViewModel vm = ServiceProvider.GetRequiredService<NugetDetailsViewModel>();
			vm.Title = x.Title;
			vm.Description = x.Description;
			vm.IconUrl = x.IconUrl ?? NugetDetailsViewModel.DefaultIconUri;
			vm.ProjectUrl = x.ProjectUrl;
			return vm;
		});
	}
}
