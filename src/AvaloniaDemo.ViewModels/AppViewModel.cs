using System.Globalization;

namespace AvaloniaDemo.ViewModels;

public partial class AppViewModel : ViewModelBase, ISingletonDependency
{
	[Reactive]
	public partial string? SearchTerm { get; set; }

	private readonly SourceList<NugetDetailsViewModel> _packages = new();

	[BindableDerivedList]
	private readonly ReadOnlyObservableCollection<NugetDetailsViewModel> _searchResults;

	[Reactive]
	public partial string? CurrentCulture { get; set; }

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

		Locator.Current.GetService<ObservableCultureService>()?
			.CultureChanged
			.Subscribe(_ => CurrentCulture = CultureInfo.CurrentCulture.DisplayName);
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
			vm.Description = GetDescription(x.Description);
			vm.IconUrl = x.IconUrl ?? NugetDetailsViewModel.DefaultIconUri;
			vm.ProjectUrl = x.ProjectUrl;
			return vm;
		});

		string? GetDescription(string? description)
		{
			if (description.IsNullOrWhiteSpace())
			{
				return null;
			}

			int i = description.IndexOf('\n');

			if (i < 0)
			{
				return description;
			}

			return description.Substring(0, i);
		}
	}

	[ReactiveCommand]
	private void SwitchLanguage()
	{
		ObservableCultureService service = TransientCachedServiceProvider.GetRequiredService<ObservableCultureService>();

		CultureInfo en = new("en");

		service.ChangeCulture(Equals(CultureInfo.CurrentCulture, en) ? new CultureInfo("zh-CN") : en);
	}
}
